using Clinic.Application.Contracts.Persistence;
using Clinic.Application.Exceptions;
using MediatR;

namespace Clinic.Application.Features.Appointment.Commands.DeleteAppointment;

public class DeleteAppointmentHandler : IRequestHandler<DeleteAppointmentCommand, Unit>
{
    private readonly IAppointmentRepository _leaveTypeRepository;

    public DeleteAppointmentHandler(IAppointmentRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        // retrieve domain entity object
        var appointmentToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id);
        
        // verify that record exists
        if (appointmentToDelete == null)
            throw new NotFoundException(nameof(Appointment), request.Id);

        // remove from database
        await _leaveTypeRepository.DeleteAsync(appointmentToDelete);

        // retun record id
        return Unit.Value;
    }
}