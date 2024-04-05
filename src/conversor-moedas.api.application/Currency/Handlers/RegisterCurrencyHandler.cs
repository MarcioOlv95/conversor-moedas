using AutoMapper;
using conversor_moedas.api.application.Common.Notifier;
using conversor_moedas.api.application.Currency.Messaging.Requests;
using conversor_moedas.domain.Enums;
using conversor_moedas.domain.Repositories;
using conversor_moedas.domain.Shared;
using MediatR;

namespace conversor_moedas.api.application.Currency.Handlers
{
    public class RegisterCurrencyHandler : IRequestHandler<RegisterCurrencyRequest, Result>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterCurrencyHandler(
            ICurrencyRepository currencyRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> Handle(RegisterCurrencyRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var alreadyExist = await _currencyRepository.AnyAsync(c => c.Name.Contains(request.Name), cancellationToken);

                if (alreadyExist)
                    return Result.Failure("Currency already exists.");

                _currencyRepository.Add(new domain.Entities.Currency(request.Name, request.Description));

                await _unitOfWork.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"There was an error to insert the currency. Details: {ex.Message}");
            }            
        }
    }
}
