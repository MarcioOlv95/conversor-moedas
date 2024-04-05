using AutoFixture;
using conversor_moedas.api.application.Common.Notifier;
using conversor_moedas.api.application.Currency.Handlers;
using conversor_moedas.api.application.Currency.Messaging.Requests;
using conversor_moedas.api.application.Currency.Messaging.Responses;
using conversor_moedas.api.Mappings;
using conversor_moedas.api.test.Application.Common.Fakes;
using conversor_moedas.domain.Repositories;

namespace conversor_moedas.api.test.Application.Currency.Handlers
{
    public class GetCurrencyHandlerTest
    {
        private readonly Mock<ICurrencyRepository> _currencyRepository = new();
        private readonly Mock<INotifier> _notifier = new();
        private readonly GetCurrencyHandler _getCurrencyHandler;

        public GetCurrencyHandlerTest()
        {
            IConfigurationProvider configuration = new MapperConfiguration(config => config.AddProfile<DtoMappingProfile>());
            var mapper = configuration.CreateMapper();

            _getCurrencyHandler = new GetCurrencyHandler(_currencyRepository.Object, mapper, _notifier.Object);
        }

        [Fact]
        public async Task GetCurrency_Should_ReturnsPagedListResultAsync()
        {
            //Arrange
            var fixture = new Fixture();
            var pagedListResult = CurrencyFaker.GetPagedListDefault();

            const int page = 1;
            const int pageSize = 10;

            var request = fixture.Build<GetCurrencyRequest>()
                .With(x => x.page, page)
                .With(x => x.pageSize, pageSize)
                .Create();

            _currencyRepository.Setup(x => x.GetAllAsync(request.page, request.pageSize, CancellationToken.None))
                .ReturnsAsync(pagedListResult);

            //act
            var response = await _getCurrencyHandler.Handle(request, CancellationToken.None);

            //assert
            response.PageSize.Should().Be(pageSize);
            response.Page.Should().Be(page);
            response.Data.Should().HaveCountLessThanOrEqualTo(pageSize);
            response.Data.Should().BeAssignableTo<IEnumerable<CurrencyResponse>>();
        }
    }
}
