using FluentValidation;

namespace Clinic.Application.Features.MedicalRecord.Commands.CreateMedicalRecord
{
    public class CreateMedicalRecordCommandValidator : AbstractValidator<CreateMedicalRecordCommand>
    {
        public CreateMedicalRecordCommandValidator()
        {
            RuleFor(p => p.PatientId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.DoctorId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.RecordDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Record date cannot be in the future.");

            RuleFor(p => p.Diagnosis)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.Treatment)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");
        }
    }
}

