namespace BTG.Domain.Contracts.RabbitMQ
{
    public interface IProcessadorMensagem
    {
        Task HandleMensagemAsync(string fila, string conteudo);
    }
}
