using BTG.Application.Ioc;
using BTG.Infrastructure.Ioc;

namespace BTG.Api
{
    public static class Ioc
    {
        public static void RegisterDI(this IServiceCollection services)
        {
            ServicesIoc.Register(services);
            RepositoriesIoc.Register(services);
        }
    }
}
