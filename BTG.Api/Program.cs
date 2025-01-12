using BTG.Api;
using BTG.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.RegisterDI();

//Banco
builder.Services.AddDbContext<DefaultContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
         sqlServerOptions =>
         {
             //sqlServerOptions.MigrationsAssembly("API");
         }
     ), ServiceLifetime.Scoped, ServiceLifetime.Scoped
 );

var app = builder.Build();

MigrationStart.Population(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
