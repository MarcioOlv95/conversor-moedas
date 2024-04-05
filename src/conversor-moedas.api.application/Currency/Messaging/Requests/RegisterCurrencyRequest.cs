using conversor_moedas.domain.Shared;
using MediatR;

namespace conversor_moedas.api.application.Currency.Messaging.Requests
{
    public class RegisterCurrencyRequest : IRequest<Result>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
