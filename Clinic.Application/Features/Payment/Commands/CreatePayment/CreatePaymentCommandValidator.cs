using FluentValidation;

namespace Clinic.Application.Features.Payment.Commands.CreatePayment
{
    public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentCommandValidator()
        {
            RuleFor(p => p.PatientId)
                .NotEmpty().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Amount)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(p => p.PaymentMethod)
                .IsInEnum().WithMessage("{PropertyName} is not a valid payment method.");

            RuleFor(p => p.Status)
                .IsInEnum().WithMessage("{PropertyName} is not a valid payment status.");
        }
    }
}

