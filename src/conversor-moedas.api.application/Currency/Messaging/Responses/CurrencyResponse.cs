using conversor_moedas.domain.Shared;

namespace conversor_moedas.api.application.Currency.Messaging.Responses
{
    public class CurrencyResponse : Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}