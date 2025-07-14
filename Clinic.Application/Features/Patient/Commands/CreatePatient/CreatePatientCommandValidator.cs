using FluentValidation;

namespace Clinic.Application.Features.Patient.Commands.CreatePatient
{
    /// <summary>
    /// FluentValidation validator for CreatePatientCommand.
    /// Ensures all required fields are provided and meet business rules.
    /// </summary>
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .EmailAddress().WithMessage("{PropertyName} must be a valid email address.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.PhoneNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("{PropertyName} must be a valid phone number.");

            RuleFor(p => p.Address)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .LessThan(DateTime.Today).WithMessage("{PropertyName} must be in the past.")
                .GreaterThan(DateTime.Today.AddYears(-120)).WithMessage("{PropertyName} must be within the last 120 years.");

            RuleFor(p => p.IdentityUserId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.PatientIdNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters.");

            RuleFor(p => p.InsuranceProvider)
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.")
                .When(p => !string.IsNullOrEmpty(p.InsuranceProvider));

            RuleFor(p => p.InsurancePolicyNumber)
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .When(p => !string.IsNullOrEmpty(p.InsurancePolicyNumber));

            RuleFor(p => p.EmergencyContactName)
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.")
                .When(p => !string.IsNullOrEmpty(p.EmergencyContactName));

            RuleFor(p => p.EmergencyContactPhone)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("{PropertyName} must be a valid phone number.")
                .When(p => !string.IsNullOrEmpty(p.EmergencyContactPhone));

            RuleFor(p => p.BloodType)
                .Must(BeValidBloodType).WithMessage("{PropertyName} must be a valid blood type (A+, A-, B+, B-, AB+, AB-, O+, O-).")
                .When(p => !string.IsNullOrEmpty(p.BloodType));

            RuleFor(p => p.Allergies)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.")
                .When(p => !string.IsNullOrEmpty(p.Allergies));
        }

        private bool BeValidBloodType(string? bloodType)
        {
            if (string.IsNullOrEmpty(bloodType))
                return true;

            var validBloodTypes = new[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
            return validBloodTypes.Contains(bloodType.ToUpper());
        }
    }
}

