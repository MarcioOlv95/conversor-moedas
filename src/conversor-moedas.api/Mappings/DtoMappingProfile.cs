using AutoMapper;
using conversor_moedas.api.application.Currency.Messaging.Responses;
using conversor_moedas.domain.Entities;

namespace conversor_moedas.api.Mappings
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile() 
        {
            CreateMap<Currency, CurrencyResponse>();
        }
    }
}
