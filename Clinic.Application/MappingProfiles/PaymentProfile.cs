
using AutoMapper;
using Clinic.Application.Features.Payment.Commands.CreatePayment;
using Clinic.Application.Features.Payment.Queries.GetPaymentDetail;
using Clinic.Domain.Entities;

namespace Clinic.Application.MappingProfiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentDto, Payment>().ReverseMap();
            CreateMap<Payment, PaymentDetailDto>();
            CreateMap<CreatePaymentCommand, Payment>();
        }
    }
}

