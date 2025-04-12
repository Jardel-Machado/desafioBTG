namespace DesafioBtg.Consumers.Usuarios.Interfaces;

public interface IUsuarioConsumer
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}
