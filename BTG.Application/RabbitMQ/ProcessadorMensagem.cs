using BTG.Domain.Contracts.RabbitMQ;
using BTG.Domain.Models;

namespace BTG.Application.RabbitMQ
{
    public class ProcessadorMensagem : IProcessadorMensagem
    {
        private readonly IProcessadorMensagemService _processadorMensagemService;
        public ProcessadorMensagem(IProcessadorMensagemService processadorMensagemService)
        {
            _processadorMensagemService = processadorMensagemService;
        }

        public async Task HandleMensagemAsync(string fila, string conteudo)
        {
            var msg = new Mensagem { FilaNome = fila, Conteudo = conteudo };
            await _processadorMensagemService.ProcessarMensagemAsync(msg);
        }
    }
}
