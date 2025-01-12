using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BTG.Infrastructure
{
    public static class MigrationStart
    {
        public static void Population(this IApplicationBuilder app)
        {
            using (var service = app.ApplicationServices.CreateScope())
            {
                SeedData(service.ServiceProvider.GetService<DefaultContext>());
            }
        }
        public static void SeedData(DefaultContext context)
        {
            Console.WriteLine("Aplicando Migrations...");
            context.Database.Migrate();
            Console.WriteLine("Aplicado com sucesso");
        }
    }
}
