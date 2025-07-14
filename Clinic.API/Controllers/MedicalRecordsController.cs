using MediatR;
using Microsoft.AspNetCore.Mvc;
using Clinic.Application.Features.MedicalRecord.Commands.CreateMedicalRecord;
using Clinic.Application.Features.MedicalRecord.Queries.GetMedicalRecordDetail;

namespace Clinic.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicalRecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MedicalRecordDetailDto>> Get(int id)
        {
            var medicalRecord = await _mediator.Send(new GetMedicalRecordDetailQuery { Id = id });
            return Ok(medicalRecord);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CreateMedicalRecordCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
    }
}

