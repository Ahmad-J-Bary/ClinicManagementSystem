
using FluentValidation;

namespace Clinic.Application.Features.Admin.Commands.CreateAdmin
{
    public class CreateAdminCommandValidator : AbstractValidator<CreateAdminCommand>
    {
        public CreateAdminCommandValidator()
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
        }
    }
}

