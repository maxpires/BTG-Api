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
using System.Net.Sockets;

var connectionStringBD = "Server=sqlserver,1433;Database=db_btg; TrustServerCertificate=True; User ID=sa;Password=Desafio@BTG";
var connectionStringRabbit = "amqp://guest:guest@rabbitmq:5672";

Console.WriteLine("###########################");
Console.WriteLine("#       DESAFIO BTG       #");
Console.WriteLine("#  Consumo Fila RabbitMQ  #");
Console.WriteLine("###########################");
Console.WriteLine("");
Console.WriteLine("Iniciando Microserviço");

bool IsServiceAvailable(string host, int port)
{
    try
    {
        using (var client = new TcpClient())
        {
            client.Connect(host, port);
            return true;
        }
    }
    catch
    {
        return false;
    }
}

Console.WriteLine("Aguardando serviços...");
while (!IsServiceAvailable("rabbitmq", 5672) || !IsServiceAvailable("sqlserver", 1433))
{
    Console.WriteLine("Serviços não disponíveis. Tentando novamente em 10 segundos...");
    Thread.Sleep(10000);
}

Console.WriteLine("Todos os serviços estão disponíveis. Iniciando a aplicação.");


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


