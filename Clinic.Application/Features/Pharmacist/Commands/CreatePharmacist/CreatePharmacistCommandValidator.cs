using FluentValidation;

namespace Clinic.Application.Features.Pharmacist.Commands.CreatePharmacist
{
    public class CreatePharmacistCommandValidator : AbstractValidator<CreatePharmacistCommand>
    {
        public CreatePharmacistCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("A valid email is required.");

            RuleFor(p => p.PharmacyLicenseNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}

