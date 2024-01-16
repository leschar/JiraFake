using JiraFake.Domain.AppSettings;
using JiraFake.Domain.Communications.RabbitMq.Patterns.Factory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace JiraFake.Application.Worker
{
    public class RabbitMqWorker : BackgroundService
    {
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly ILogger<RabbitMqWorker> _logger;
        private readonly IFilaFactory _filaFactory;

        public RabbitMqWorker(IOptions<RabbitMqSettings> rabbitMqSettings, ILogger<RabbitMqWorker> logger, IFilaFactory filaFactory)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
            _logger = logger;
            _filaFactory = filaFactory;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(_rabbitMqSettings.ConnectionString)
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                foreach (var filaConfig in _rabbitMqSettings.Filas)
                {
                    channel.QueueDeclare(queue: filaConfig.Key, durable: true, exclusive: false, autoDelete: false, arguments: null);
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += async (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        _logger.LogInformation($"Mensagem recebida da fila {filaConfig.Key} com a rota {filaConfig.Value.RoutingKey}: {message}");

                        var fila = _filaFactory.CriarFila(filaConfig.Value.RoutingKey);
                        fila.Processar(message);

                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    channel.BasicConsume(queue: filaConfig.Key, autoAck: false, consumer: consumer);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
