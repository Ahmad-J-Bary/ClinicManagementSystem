using MediatR;

namespace Clinic.Application.Features.Appointment.Commands.DeleteAppointment;

public class DeleteAppointmentCommand : IRequest<Unit>
{
    public int Id { get; set; }
}