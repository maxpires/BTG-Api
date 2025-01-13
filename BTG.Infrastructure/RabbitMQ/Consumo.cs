using BTG.Domain.Contracts.RabbitMQ;
using BTG.Domain.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;

namespace BTG.Infrastructure.RabbitMQ
{
    public class Consumo
    {
        private readonly string _uri;
        private readonly string _fila;
        private readonly IProcessadorMensagemService _processadorMensagemService;

        public Consumo(string uri, string fila, IProcessadorMensagemService processadorMensagemService)
        {
            _processadorMensagemService = processadorMensagemService;
            _uri = uri;
            _fila = fila;
        }

        public async Task Executa()
        {           
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_uri),
                RequestedConnectionTimeout = TimeSpan.FromSeconds(30),
                AutomaticRecoveryEnabled = true,
            };

            try
            {
                using var conexao = await factory.CreateConnectionAsync();
                using var channel = await conexao.CreateChannelAsync();

                await channel.QueueDeclareAsync(_fila, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, evt) =>
                {
                    var message = Encoding.UTF8.GetString(evt.Body.ToArray());

                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    Console.WriteLine($"[{ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") }] Inicia Processamento Fila");

                    var resp = await _processadorMensagemService.ProcessarMensagemAsync(new Mensagem { FilaNome = _fila, Conteudo = message });
                        
                    if (resp)
                        Console.WriteLine($"Processado com Sucesso");

                    stopwatch.Stop();
                    Console.WriteLine($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}] Fim Processamento. Processado em: { stopwatch.ElapsedMilliseconds }ms");
                };

                _ = channel.BasicConsumeAsync(_fila, autoAck: true, consumer);
                    
                Console.WriteLine($"Iniciado com sucesso");
                await Task.Delay(-1);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na Iniciação com o RabbitMQ. Detalhes: {ex.Message}");
                await Task.Delay(TimeSpan.FromSeconds(10));
            }

            while (true) { Task.Delay(100).Wait(); }
        }        
    }
}
