using DesafioBtg.Consumers.Usuarios.Interfaces;
using DesafioBtg.Ioc.Abstracoes;

namespace DesafioBtg.Consumers.Usuarios;

public class UsuariosBackgroundService(IServiceProvider serviceProvider, ILogger<UsuariosBackgroundService> logger) : BackgroundServiceBase<UsuariosBackgroundService>(logger)
{
    protected override async Task Process(CancellationToken cancellationToken)
    {
        internalCancellationToken = cancellationToken;

        using var scope = serviceProvider.CreateScope();

        var executor = scope.ServiceProvider.GetRequiredService<IUsuarioConsumer>();

        await executor.ExecuteAsync(cancellationToken);
    }
}
