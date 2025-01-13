using BTG.Domain.Contracts.RabbitMQ;
using BTG.Domain.Contracts.Repositories;
using BTG.Domain.InputOutput;
using BTG.Domain.Models;
using System.Text.Json;

namespace BTG.Application.RabbitMQ
{
    public class ProcessadorMensagemService : IProcessadorMensagemService
    {
        private readonly IPedidoRepository _pedidoRepository;
        public ProcessadorMensagemService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository ?? throw new ArgumentNullException(nameof(pedidoRepository));
        }
        public async Task<bool> ProcessarMensagemAsync(Mensagem mensagem)
        {
            try
            {
                var pedido = JsonSerializer.Deserialize<PedidoInput>(mensagem.Conteudo);
                return await _pedidoRepository.Inserir(pedido);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar a Fila. Detalhes: { ex.Message }");
                return false;
            }
        }
    }
}
