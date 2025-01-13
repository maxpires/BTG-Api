using BTG.Domain.Models;

namespace BTG.Domain.Contracts.RabbitMQ
{
    public interface IProcessadorMensagemService
    {
        Task<bool> ProcessarMensagemAsync(Mensagem mensagem);
    }
}
