using conversor_moedas.domain.Shared;
using System.Linq.Expressions;

namespace conversor_moedas.domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IPagedResult<TEntity>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
