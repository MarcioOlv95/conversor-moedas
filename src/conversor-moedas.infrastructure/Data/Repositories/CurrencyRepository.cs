using conversor_moedas.domain.Entities;
using conversor_moedas.domain.Repositories;
using conversor_moedas.infrastructure.Data.Context;

namespace conversor_moedas.infrastructure.Data.Repositories
{
    public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
    {
        private readonly ConversorMoedasDbContext _conversorMoedasDbContext;

        public CurrencyRepository(ConversorMoedasDbContext context) : base(context)
        {
            _conversorMoedasDbContext = context;
        }
    }
}
