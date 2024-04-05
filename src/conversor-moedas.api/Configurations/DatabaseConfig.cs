using conversor_moedas.infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace conversor_moedas.api.Configurations
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, 
            IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ConversorMoedasDbContext>(options =>
            {
                options.UseMySQL(connectionString);
            });

            return services;
        }
    }
}
