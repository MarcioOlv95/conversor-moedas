using AutoMapper;
using conversor_moedas.api.application.Common.Mappings;
using conversor_moedas.api.application.Common.Notifier;
using conversor_moedas.api.application.Common.Pagination;
using conversor_moedas.api.application.Currency.Messaging.Requests;
using conversor_moedas.api.application.Currency.Messaging.Responses;
using conversor_moedas.domain.Repositories;
using MediatR;

namespace conversor_moedas.api.application.Currency.Handlers
{
    public class GetCurrencyHandler : IRequestHandler<GetCurrencyRequest, PagedResult<CurrencyResponse>>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;
        private readonly INotifier _notifier;

        public GetCurrencyHandler(ICurrencyRepository currencyRepository, IMapper mapper, INotifier notifier)
        {
            _currencyRepository = currencyRepository;
            _mapper = mapper;
            _notifier = notifier;
        }

        public async Task<PagedResult<CurrencyResponse>> Handle(GetCurrencyRequest request, CancellationToken cancellationToken)
        {
            var currencys = await _currencyRepository.GetAllAsync(request.page, request.pageSize, cancellationToken);

            return _mapper.MapToPagedResult<CurrencyResponse>(currencys);
        }
    }
}
