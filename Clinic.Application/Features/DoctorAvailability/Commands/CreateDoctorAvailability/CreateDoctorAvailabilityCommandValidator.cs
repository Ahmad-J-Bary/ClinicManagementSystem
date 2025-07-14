using FluentValidation;

namespace Clinic.Application.Features.DoctorAvailability.Commands.CreateDoctorAvailability
{
    public class CreateDoctorAvailabilityCommandValidator : AbstractValidator<CreateDoctorAvailabilityCommand>
    {
        public CreateDoctorAvailabilityCommandValidator()
        {
            RuleFor(p => p.DoctorId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.StartTime)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(BeAValidDate).WithMessage("Start time must be a valid future date.");

            RuleFor(p => p.EndTime)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(p => p.StartTime).WithMessage("End time must be after start time.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date > DateTime.Now;
        }
    }
}

