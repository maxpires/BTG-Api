using BTG.Domain.Contracts.Repositories;
using BTG.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BTG.Infrastructure.Ioc
{
    public class RepositoriesIoc
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IPedidoRepository, PedidoRepository>();
        }
    }
}
