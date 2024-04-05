using AutoMapper;
using conversor_moedas.api.application.Common.Pagination;
using conversor_moedas.domain.Repositories;
using conversor_moedas.domain.Shared;

namespace conversor_moedas.api.application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static PagedResult<TResponse> MapToPagedResult<TResponse>(this IMapper mapper, IPagedResult<Entity> pagedResult)
        {
            var items = mapper.Map<IEnumerable<TResponse>>(pagedResult);

            return new PagedResult<TResponse>(items, pagedResult.Page, pagedResult.PageSize, pagedResult.PageCount, pagedResult.TotalCount);
        }
    }
}
