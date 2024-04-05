using conversor_moedas.api.application.Common.Commands;
using conversor_moedas.api.application.Common.Notifier;
using conversor_moedas.api.application.Currency.Messaging.Requests;
using conversor_moedas.api.application.Currency.Messaging.Responses;
using conversor_moedas.domain.Integrations.Api.CurrencyApi;
using conversor_moedas.domain.Repositories;
using conversor_moedas.domain.Shared;

namespace conversor_moedas.api.application.Currency.Handlers
{
    public class ConvertCurrencyHandler : ICommandHandler<ConvertCurrencyRequest, ConvertCurrencyResponse>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ICurrencyApiManager _currencyApiManager;
        private readonly INotifier _notifier;

        public ConvertCurrencyHandler(ICurrencyRepository currencyRepository,
                                        INotifier notifier,
                                        ICurrencyApiManager currencyApiManager)
        {
            _currencyRepository = currencyRepository;
            _notifier = notifier;
            _currencyApiManager = currencyApiManager;
        }

        public async Task<Result<ConvertCurrencyResponse>> Handle(ConvertCurrencyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var erros = await ValidateCurrenciesAsync(request);

                if (erros.Any())
                    return Result.Failure<ConvertCurrencyResponse>(erros.ToList());

                var result = await CalculateConversionAsync(request);

                return new ConvertCurrencyResponse() { Value = result };
            }
            catch (Exception ex)
            {
                return Result.Failure<ConvertCurrencyResponse>($"There was an error to convert the currency. Details: {ex.Message}");
            }
        }

        private async Task<double> CalculateConversionAsync(ConvertCurrencyRequest request)
        {
            var valueCurrencyBase = await _currencyApiManager.GetCurrencyValue(request.CurrencyTo, request.CurrencyFrom);
            valueCurrencyBase = decimal.Round(valueCurrencyBase, 2, MidpointRounding.AwayFromZero);

            var valueConverted = request.Amount * (double)valueCurrencyBase;

            return valueConverted;
        }

        private async Task<IEnumerable<string>> ValidateCurrenciesAsync(ConvertCurrencyRequest request)
        {
            var allCurencies = await _currencyRepository.GetAllAsync();

            var erros = new List<string>();

            if (!allCurencies.Any(x => x.Name.Equals(request.CurrencyTo, StringComparison.OrdinalIgnoreCase)))
                erros.Add("Currency to convert not found");
            
            if (!allCurencies.Any(x => x.Name.Equals(request.CurrencyFrom, StringComparison.OrdinalIgnoreCase)))
                erros.Add("Currency from not found");

            return erros;
            
        }

    }
}
