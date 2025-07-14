using FluentValidation;

namespace Clinic.Application.Features.Admin.Commands.UpdateAdmin
{
    public class UpdateAdminCommandValidator : AbstractValidator<UpdateAdminCommand>
    {
        public UpdateAdminCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotNull().WithMessage("{PropertyName} is required.");

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

