using AutoFixture;
using conversor_moedas.api.application.Common.Notifier;
using conversor_moedas.api.application.Currency.Handlers;
using conversor_moedas.api.application.Currency.Messaging.Requests;
using conversor_moedas.api.application.Currency.Messaging.Responses;
using conversor_moedas.domain.Integrations.Api.CurrencyApi;
using conversor_moedas.domain.Repositories;
using conversor_moedas.domain.Shared;

namespace conversor_moedas.api.test.Application.Currency.Handlers
{
    public class ConvertCurrencyHandlerTest
    {
        private readonly Mock<ICurrencyRepository> _currencyRepository = new();
        private readonly Mock<INotifier> _notifier = new();
        private readonly Mock<ICurrencyApiManager> _currencyApiManager = new();
        private readonly ConvertCurrencyHandler _convertCurrencyHandler;

        public ConvertCurrencyHandlerTest()
        {
            _convertCurrencyHandler = new ConvertCurrencyHandler(_currencyRepository.Object, _notifier.Object, _currencyApiManager.Object);
        }

        [Fact]
        public async Task ConvertCurrency_Shoud_ReturnCurrencyConvertedAsync()
        {
            //arrange
            var fixture = new Fixture();
            var request = fixture.Create<ConvertCurrencyRequest>();
            var listCurrencys = new List<domain.Entities.Currency>()
            {
                new domain.Entities.Currency(request.CurrencyFrom, string.Empty),
                new domain.Entities.Currency(request.CurrencyTo, string.Empty)
            };

            _currencyRepository.Setup(currency => currency.GetAllAsync(CancellationToken.None))
                .ReturnsAsync(listCurrencys);

            _currencyApiManager.Setup(currency => currency.GetCurrencyValue(request.CurrencyTo, request.CurrencyFrom))
                .ReturnsAsync(fixture.Create<decimal>());

            //act
            var response = await _convertCurrencyHandler.Handle(request, CancellationToken.None);

            //assert
            Assert.IsType<ConvertCurrencyResponse>(response.Value);
            Assert.IsType<double>(response.Value.Value);
        }

        [Fact]
        public async Task ConvertCurrency_Shoud_ReturnCurrencyNotFoundAsync()
        {
            //arrange
            var fixture = new Fixture();
            var request = fixture.Build<ConvertCurrencyRequest>()
                            .With(x => x.CurrencyFrom, "Currency not exist")
                            .Create();

            _currencyRepository.Setup(currency => currency.GetAllAsync(CancellationToken.None))
                .ReturnsAsync(fixture.Create<List<domain.Entities.Currency>>());

            //act
            var response = await _convertCurrencyHandler.Handle(request, CancellationToken.None);

            //assert
            response.Should().BeOfType<Result<ConvertCurrencyResponse>>();
            response.IsFailure.Should().BeTrue();
            response.Errors.Should().HaveCount(2);
        }
    }
}
