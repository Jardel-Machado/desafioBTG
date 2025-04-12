namespace DesafioBtg.Dominio.Rabbitmq.Repositorios.Interfaces
{
    public interface IRabbitmqRepositorio
    {
        Task PublicarMensagemAsync(string mensagem, string topico, CancellationToken cancellationToken);
        Task ConsumirMensagemAsync(string fila, Func<string, Task> callback, CancellationToken cancellationToken);
    }
}
