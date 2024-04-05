namespace conversor_moedas.domain.Integrations.Api.CurrencyApi
{
    public interface ICurrencyApiManager
    {
        Task<decimal> GetCurrencyValue(string currencyTo, string currencyFrom);
    }
}
