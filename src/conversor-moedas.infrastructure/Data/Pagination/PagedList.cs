using conversor_moedas.domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace conversor_moedas.infrastructure.Data.Pagination
{
    public class PagedList<T> : List<T>, IPagedResult<T>
    {
        public PagedList() { }
        public PagedList(int totalCount, int page, int pageSize, IEnumerable<T> results)
        {
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
            AddRange(results);
        }
        public int Page { get; }
        public int PageSize { get; }
        public int PageCount { get; }
        public int TotalCount { get; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken);
            var skip = ((page - 1)) * pageSize;

            var results = await source
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedList<T>(count, page, pageSize, results);
        }
    }
}
