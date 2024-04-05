using conversor_moedas.domain.Repositories;
using conversor_moedas.infrastructure.Data.Context;
using System;

namespace conversor_moedas.infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConversorMoedasDbContext _context;

        public UnitOfWork(ConversorMoedasDbContext context)
        {
            _context = context;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public Task BeginTransaction()
        {
            return _context.Database.BeginTransactionAsync();
        }

        public async Task<bool> Commit(CancellationToken cancellationToken)
        {
            if (_context.Database.CurrentTransaction is null)
            {
                return false;
            }

            try
            {
                await _context.Database.CurrentTransaction.CommitAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                await Rollback(cancellationToken);
                return false;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task Rollback(CancellationToken cancellationToken)
        {
            if (_context.Database.CurrentTransaction is null)
            {
                return Task.CompletedTask;
            }

            return _context.Database.CurrentTransaction.RollbackAsync(cancellationToken);
        }
    }
}
