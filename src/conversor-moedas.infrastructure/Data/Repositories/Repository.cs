using conversor_moedas.domain.Repositories;
using conversor_moedas.domain.Shared;
using conversor_moedas.infrastructure.Data.Context;
using conversor_moedas.infrastructure.Data.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace conversor_moedas.infrastructure.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        private readonly ConversorMoedasDbContext _context;

        public Repository(ConversorMoedasDbContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().ToListAsync(cancellationToken);
        }

        public async Task<IPagedResult<TEntity>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var query = GetQueryable();
            return await PagedList<TEntity>.CreateAsync(query, page, pageSize, cancellationToken);
        }

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().Where(predicate).AnyAsync(cancellationToken);
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _context.Set<TEntity>();
        }
    }
}
