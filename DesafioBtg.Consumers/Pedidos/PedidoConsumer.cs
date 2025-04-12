using DesafioBtg.Consumers.Pedidos.Interfaces;
using DesafioBtg.Dominio.Pedidos.Servicos.Comandos;
using DesafioBtg.Dominio.Pedidos.Servicos.Interfaces;
using DesafioBtg.Dominio.Rabbitmq.Repositorios.Interfaces;
using System.Text.Json;

namespace DesafioBtg.Consumers.Pedidos;

public class PedidoConsumer(ILogger<PedidoConsumer> logger, IRabbitmqRepositorio rabbitmqRepositorio, IServiceScopeFactory scopeFactory) : IPedidoConsumer
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("<{EventoId}> Aguardando mensagens da fila 'btg-pactual-order-created'...", "PedidoConsumer");

        await rabbitmqRepositorio.ConsumirMensagemAsync("btg-pactual-order-created", async (mensagem) =>
        {
            using var scope = scopeFactory.CreateScope();

            var pedidosServico = scope.ServiceProvider.GetRequiredService<IPedidosServico>();

            try
            {
                logger.LogInformation("<{EventoId}> Mensagem recebida: {Mensagem}", "PedidoConsumer", mensagem);

                JsonSerializerOptions options = new() 
                {
                    PropertyNameCaseInsensitive = true
                };

                PedidoComando? pedido = JsonSerializer.Deserialize<PedidoComando>(mensagem, options);

                if (pedido is null)
                {
                    logger.LogError("<{EventoId}> Falha ao Deserialize: {Mensagem}", "PedidoConsumer", mensagem);

                    return;
                }

                logger.LogInformation("<{EventoId}> Iniciando processamento do pedido...", "PedidoConsumer");

                await pedidosServico.InserirAsync(pedido!, cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "<{EventoId}> Falha ao processar mensagem da fila.", "PedidoConsumer");
            }
        }, cancellationToken);
    }
}
