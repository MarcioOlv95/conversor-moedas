namespace conversor_moedas.api.application.Common.Pagination
{
    public class PagedResult<TResponse>
    {
        public PagedResult(IEnumerable<TResponse> items, int page, int pageSize, int pageCount, int totalCount)
        {
            Data = items;
            Page = page;
            PageSize = pageSize;
            PageCount = pageCount;
            TotalCount = totalCount;
        }

        public IEnumerable<TResponse> Data { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int PageCount { get; }
        public int TotalCount { get; }
        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < PageCount;
    }
}
