using BTG.Application.Services;
using BTG.Domain.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BTG.Application.Ioc
{
    public static class ServicesIoc
    {
        public static void Register(IServiceCollection services)
        {
            services.AddTransient<IPedidoService, PedidoService>();
        }
    }
}
