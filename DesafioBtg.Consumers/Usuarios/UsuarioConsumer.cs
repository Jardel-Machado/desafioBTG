using DesafioBtg.Consumers.Usuarios.Interfaces;
using DesafioBtg.Dominio.Rabbitmq.Repositorios.Interfaces;

namespace DesafioBtg.Consumers.Usuarios;

public class UsuarioConsumer(ILogger<UsuarioConsumer> logger, IRabbitmqRepositorio rabbitmqRepositorio) : IUsuarioConsumer
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        
    }   
}
