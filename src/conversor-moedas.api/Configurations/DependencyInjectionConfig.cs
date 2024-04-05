using conversor_moedas.api.application.Common;
using conversor_moedas.api.application.Common.Notifier;
using conversor_moedas.domain.Integrations.Api.CurrencyApi;
using conversor_moedas.domain.Repositories;
using conversor_moedas.infrastructure.Data;
using conversor_moedas.infrastructure.Data.Integrations.Apis.CurrencyApi;
using conversor_moedas.infrastructure.Data.Repositories;

namespace conversor_moedas.api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void ResolveDependencies(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(AssemblyReference.Assembly);
            });

            services.AddAutoMapper(AssemblyReference.Assembly);

            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICurrencyApiManager, CurrencyApiManager>();
        }
    }
}
