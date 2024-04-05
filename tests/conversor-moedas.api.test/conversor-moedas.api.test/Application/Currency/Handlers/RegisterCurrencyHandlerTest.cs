using AutoFixture;
using conversor_moedas.api.application.Common.Notifier;
using conversor_moedas.api.application.Currency.Handlers;
using conversor_moedas.api.application.Currency.Messaging.Requests;
using conversor_moedas.api.Mappings;
using conversor_moedas.domain.Repositories;
using System.Linq.Expressions;

namespace conversor_moedas.api.test.Application.Currency.Handlers
{
    public class RegisterCurrencyHandlerTest
    {
        private readonly Mock<ICurrencyRepository> _currencyRepository = new();
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly RegisterCurrencyHandler _registerCurrencyHandler;

        public RegisterCurrencyHandlerTest()
        {
            IConfigurationProvider configuration = new MapperConfiguration(config => config.AddProfile<DtoMappingProfile>());
            var mapper = configuration.CreateMapper();

            _registerCurrencyHandler = new RegisterCurrencyHandler(_currencyRepository.Object, _unitOfWork.Object, mapper);
        }

        [Fact]
        public async Task RegisterCurrency_Should_ReturnsSuccessAsync()
        {
            //Arrange
            var fixture = new Fixture();
            var newCurrency = fixture.Create<domain.Entities.Currency>();

            var request = fixture.Build<RegisterCurrencyRequest>().Create();

            _currencyRepository.Setup(x => x.Add(newCurrency));
                
            //act
            var response = await _registerCurrencyHandler.Handle(request, CancellationToken.None);

            //assert
            _unitOfWork.Verify();
            response.IsSuccess.Should().BeTrue();
            response.Errors.Should().BeNull();
        }

        [Fact]
        public async Task RegisterCurrency_Should_ReturnAlreadyExistsAsync()
        {
            //Arrange
            var fixture = new Fixture();
            var newCurrency = fixture.Create<domain.Entities.Currency>();

            var request = fixture.Build<RegisterCurrencyRequest>().Create();

            _currencyRepository.Setup(x => x.AnyAsync(It.IsAny<Expression<Func<domain.Entities.Currency, bool>>>(), CancellationToken.None))
                .ReturnsAsync(true);

            //act
            var response = await _registerCurrencyHandler.Handle(request, CancellationToken.None);

            //assert
            response.IsFailure.Should().BeTrue();
            response.Errors.Should().HaveCount(1);
            response.Errors?.Contains("already exists");
        }
    }
}
