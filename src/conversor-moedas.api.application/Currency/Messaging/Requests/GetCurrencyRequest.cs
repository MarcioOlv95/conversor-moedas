using conversor_moedas.api.application.Common.Pagination;
using conversor_moedas.api.application.Currency.Messaging.Responses;
using conversor_moedas.domain.Shared;
using MediatR;

namespace conversor_moedas.api.application.Currency.Messaging.Requests
{
    public class GetCurrencyRequest : IRequest<PagedResult<CurrencyResponse>>
    {
        public GetCurrencyRequest(int? page, int? pageSize)
        {
            this.page = page ?? 1;
            this.pageSize = pageSize ?? 10;
        }

        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
