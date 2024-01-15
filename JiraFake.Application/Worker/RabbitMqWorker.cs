using JiraFake.Domain.AppSettings;
using JiraFake.Domain.Communications.RabbitMq;
using JiraFake.Domain.Enum;
using JiraFake.Domain.Interfaces.Rabbit;
using JiraFake.Domain.Utils;
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
        private readonly IFilaRabbit _fila;

        public RabbitMqWorker(IOptions<RabbitMqSettings> rabbitMqSettings, ILogger<RabbitMqWorker> logger, IFilaRabbit fila)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
            _logger = logger;
            _fila = fila;
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

                        if (filaConfig.Value.RoutingKey == EnumUtils.ObterDescricaoEnum(FilaRabbitMqEnum.Tarefa))
                        {
                            _fila.Processar(message);
                        }
                        else if (filaConfig.Value.RoutingKey == EnumUtils.ObterDescricaoEnum(FilaRabbitMqEnum.SubTarefa))
                        {
                            _fila.Processar(message);
                        }
                        else
                        {
                            throw new ArgumentException("Fila não identificada");
                        }

                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    channel.BasicConsume(queue: filaConfig.Key, autoAck: false, consumer: consumer);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }

}
