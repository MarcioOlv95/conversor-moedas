using AutoFixture;
using conversor_moedas.infrastructure.Data.Pagination;

namespace conversor_moedas.api.test.Application.Common.Fakes
{
    public static class CurrencyFaker
    {
        public static PagedList<domain.Entities.Currency> GetPagedListDefault(int page = 1, int pageSize = 10, int count = 10)
        {
            var listCurrency = new Fixture().Create<IEnumerable<domain.Entities.Currency>>();

            return new PagedList<domain.Entities.Currency>(count, page, pageSize, listCurrency);
        }
    }
}
