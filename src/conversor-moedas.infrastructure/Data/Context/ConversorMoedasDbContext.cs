using conversor_moedas.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace conversor_moedas.infrastructure.Data.Context
{
    public class ConversorMoedasDbContext : DbContext
    {
        public ConversorMoedasDbContext(DbContextOptions<ConversorMoedasDbContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Currency> Currency => Set<Currency>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConversorMoedasDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
