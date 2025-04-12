using DesafioBtg.Dominio.Rabbitmq.Repositorios.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DesafioBtg.Infra.Rabbitmq.Repositorios
{
    public class RabbitmqRepositorio : IRabbitmqRepositorio
    {
        private readonly IConnectionFactory connectionFactory;

        public RabbitmqRepositorio(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task PublicarMensagemAsync(string mensagem, string topico, CancellationToken cancellationToken)
        {
            using var conexao = await connectionFactory.CreateConnectionAsync(cancellationToken);

            using var canal = await conexao.CreateChannelAsync(null, cancellationToken);

            await canal.QueueDeclareAsync(queue: topico, durable: true, exclusive: false, autoDelete: false, arguments: null, false, cancellationToken);

            byte[] body = Encoding.UTF8.GetBytes(mensagem);

            await canal.BasicPublishAsync(exchange: "", routingKey: topico, body: body, cancellationToken);
        }

        public async Task ConsumirMensagemAsync(string fila, Func<string, Task> callback, CancellationToken cancellationToken)
        {
            var conexao = await connectionFactory.CreateConnectionAsync(cancellationToken);

            var canal = await conexao.CreateChannelAsync(null, cancellationToken);

            await canal.QueueDeclareAsync(queue: fila, durable: true, exclusive: false, autoDelete: false, arguments: null, cancellationToken: cancellationToken);

            var consumer = new AsyncEventingBasicConsumer(canal);

            consumer.ReceivedAsync += async (model, eventArgs) =>
            {
                try
                {
                    var mensagem = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

                    await callback(mensagem);

                    await canal.BasicAckAsync(eventArgs.DeliveryTag, multiple: false, cancellationToken);
                }
                catch (Exception)
                {
                    await canal.BasicNackAsync(eventArgs.DeliveryTag, multiple: false, requeue: false, cancellationToken);
                }
            };

            await canal.BasicConsumeAsync(queue: fila, autoAck: false, consumer: consumer, cancellationToken: cancellationToken);
        }
    }
}
