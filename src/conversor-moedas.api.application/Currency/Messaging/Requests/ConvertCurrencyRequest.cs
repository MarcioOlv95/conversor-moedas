using conversor_moedas.api.application.Common.Commands;
using conversor_moedas.api.application.Currency.Messaging.Responses;

namespace conversor_moedas.api.application.Currency.Messaging.Requests
{
    public class ConvertCurrencyRequest : ICommand<ConvertCurrencyResponse>
    {
        public ConvertCurrencyRequest(string currencyFrom, string currencyTo, double amount)
        {
            CurrencyFrom = currencyFrom;
            CurrencyTo = currencyTo;
            Amount = amount;
        }

        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public double Amount { get; set; }
    }
}
