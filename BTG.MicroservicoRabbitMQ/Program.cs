using BTG.Application.RabbitMQ;
using BTG.Application.Services;
using BTG.Domain.Contracts.RabbitMQ;
using BTG.Domain.Contracts.Repositories;
using BTG.Domain.Contracts.Services;
using BTG.Infrastructure;
using BTG.Infrastructure.RabbitMQ;
using BTG.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var connectionStringBD = "Server=localhost,1444;Database=db_btg; TrustServerCertificate=True; User ID=sa;Password=Desafio@BTG";
var connectionStringRabbit = "amqp://guest:guest@localhost:5672";

Console.WriteLine("###########################");
Console.WriteLine("#       DESAFIO BTG       #");
Console.WriteLine("#  Consumo Fila RabbitMQ  #");
Console.WriteLine("###########################");
Console.WriteLine("");
Console.WriteLine("Iniciando Microserviço...");

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(log =>
    {
        log.ClearProviders();
        log.AddConsole();
        log.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.None);
    })
    .ConfigureServices((_, services) =>
    {
        services.AddScoped<IProcessadorMensagemService, ProcessadorMensagemService>();
        services.AddTransient<IPedidoService, PedidoService>();
        services.AddTransient<IPedidoRepository, PedidoRepository>();

        services.AddDbContext<DefaultContext>(opt => opt.UseSqlServer());

        services.AddDbContext<DefaultContext>(options =>
            options
                .UseSqlServer(connectionStringBD)
                .EnableSensitiveDataLogging(false), ServiceLifetime.Scoped, ServiceLifetime.Scoped
        );

        services.AddSingleton(p =>
        {
            var fila = "fila_desafio";
            var uri = connectionStringRabbit;
            var processor = p.GetRequiredService<IProcessadorMensagemService>();

            return new Consumo(uri, fila, processor);
        });
    })
    .Build();

using var scope = host.Services.CreateScope();
var c = scope.ServiceProvider.GetRequiredService<Consumo>();
await c.Executa();


