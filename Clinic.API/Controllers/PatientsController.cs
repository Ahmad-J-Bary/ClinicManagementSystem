using Clinic.Application.DTOs.Patient;
using Clinic.Application.Features.Patient.Commands.CreatePatient;
using Clinic.Application.Features.Patient.Commands.UpdatePatient;
using Clinic.Application.Features.Patient.Queries.GetPatientDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{
    /// <summary>
    /// Controller for managing patient operations.
    /// Implements CQRS pattern using MediatR for clean separation of concerns.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IMediator mediator, ILogger<PatientsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new patient in the system.
        /// </summary>
        /// <param name="createPatientDto">Patient creation data</param>
        /// <returns>The ID of the created patient</returns>
        /// <response code="201">Patient created successfully</response>
        /// <response code="400">Invalid patient data provided</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> CreatePatient([FromBody] CreatePatientDto createPatientDto)
        {
            _logger.LogInformation("Creating new patient with email: {Email}", createPatientDto.Email);

            var command = new CreatePatientCommand
            {
                FirstName = createPatientDto.FirstName,
                LastName = createPatientDto.LastName,
                Email = createPatientDto.Email,
                PhoneNumber = createPatientDto.PhoneNumber,
                Address = createPatientDto.Address,
                DateOfBirth = createPatientDto.DateOfBirth,
                IdentityUserId = createPatientDto.IdentityUserId,
                PatientIdNumber = createPatientDto.PatientIdNumber,
                InsuranceProvider = createPatientDto.InsuranceProvider,
                InsurancePolicyNumber = createPatientDto.InsurancePolicyNumber
            };

            var patientId = await _mediator.Send(command);

            _logger.LogInformation("Patient created successfully with ID: {PatientId}", patientId);

            return CreatedAtAction(nameof(GetPatient), new { id = patientId }, patientId);
        }

        /// <summary>
        /// Retrieves a patient by their ID.
        /// </summary>
        /// <param name="id">The patient's ID</param>
        /// <returns>Detailed patient information</returns>
        /// <response code="200">Patient found and returned</response>
        /// <response code="404">Patient not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PatientDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PatientDetailDto>> GetPatient(int id)
        {
            _logger.LogInformation("Retrieving patient with ID: {PatientId}", id);

            var query = new GetPatientDetailQuery(id);
            var patient = await _mediator.Send(query);

            _logger.LogInformation("Patient retrieved successfully: {PatientId}", id);

            return Ok(patient);
        }

        /// <summary>
        /// Updates an existing patient's information.
        /// </summary>
        /// <param name="id">The patient's ID</param>
        /// <param name="updatePatientDto">Updated patient data</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">Patient updated successfully</response>
        /// <response code="400">Invalid patient data provided</response>
        /// <response code="404">Patient not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] UpdatePatientDto updatePatientDto)
        {
            _logger.LogInformation("Updating patient with ID: {PatientId}", id);

            if (id != updatePatientDto.Id)
            {
                _logger.LogWarning("Patient ID mismatch: URL ID {UrlId} vs DTO ID {DtoId}", id, updatePatientDto.Id);
                return BadRequest("Patient ID mismatch");
            }

            var command = new UpdatePatientCommand
            {
                Id = updatePatientDto.Id,
                FirstName = updatePatientDto.FirstName,
                LastName = updatePatientDto.LastName,
                Email = updatePatientDto.Email,
                PhoneNumber = updatePatientDto.PhoneNumber,
                Address = updatePatientDto.Address,
                DateOfBirth = updatePatientDto.DateOfBirth,
                InsuranceProvider = updatePatientDto.InsuranceProvider,
                InsurancePolicyNumber = updatePatientDto.InsurancePolicyNumber,
                EmergencyContactName = updatePatientDto.EmergencyContactName,
                EmergencyContactPhone = updatePatientDto.EmergencyContactPhone,
                BloodType = updatePatientDto.BloodType,
                Allergies = updatePatientDto.Allergies
            };

            await _mediator.Send(command);

            _logger.LogInformation("Patient updated successfully: {PatientId}", id);

            return NoContent();
        }

        /// <summary>
        /// Retrieves all patients with pagination support.
        /// </summary>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="size">Page size (default: 10)</param>
        /// <returns>Paginated list of patients</returns>
        /// <response code="200">Patients retrieved successfully</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PatientDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetPatients(
            [FromQuery] int page = 1, 
            [FromQuery] int size = 10)
        {
            _logger.LogInformation("Retrieving patients - Page: {Page}, Size: {Size}", page, size);

            // This would be implemented with a GetPatientsQuery
            // For now, returning a placeholder response
            var patients = new List<PatientDto>();

            _logger.LogInformation("Retrieved {Count} patients", patients.Count);

            return Ok(patients);
        }

        /// <summary>
        /// Searches for patients by name, email, or patient ID.
        /// </summary>
        /// <param name="searchTerm">The search term</param>
        /// <returns>List of matching patients</returns>
        /// <response code="200">Search completed successfully</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<PatientDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PatientDto>>> SearchPatients([FromQuery] string searchTerm)
        {
            _logger.LogInformation("Searching patients with term: {SearchTerm}", searchTerm);

            // This would be implemented with a SearchPatientsQuery
            // For now, returning a placeholder response
            var patients = new List<PatientDto>();

            _logger.LogInformation("Found {Count} patients matching search term", patients.Count);

            return Ok(patients);
        }
    }
}

