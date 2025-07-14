using FluentValidation;

namespace Clinic.Application.Features.Notification.Commands.CreateNotification
{
    public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.Message)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.Type)
                .IsInEnum().WithMessage("{PropertyName} is not a valid notification type.");
        }
    }
}

