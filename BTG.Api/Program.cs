using BTG.Api;
using BTG.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.RegisterDI();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

builder.Services.AddDbContext<DefaultContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped, ServiceLifetime.Scoped
 );

builder.WebHost.ConfigureKestrel(opt => opt.ListenAnyIP(5000));

var app = builder.Build();

MigrationStart.Population(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwaggerConfig();

app.Run();
