using JiraFake.Domain.AppSettings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace JiraFake.Domain.Communications.RabbitMq
{
    public class RabbitMqSender<T>
    {
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly ILogger _logger;

        public RabbitMqSender(IOptions<RabbitMqSettings> rabbitMqSettings, ILogger<RabbitMqSender<T>> logger)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
            _logger = logger;

        }

        public async Task SendMessageAsync(T message, string fila)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_rabbitMqSettings.ConnectionString)
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(_rabbitMqSettings.Filas[fila].ExchangeName, ExchangeType.Direct, durable: true, autoDelete: false);

            var jsonMessage = System.Text.Json.JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            channel.ConfirmSelect();
            
            channel.BasicPublish(exchange: _rabbitMqSettings.Filas[fila].ExchangeName, routingKey: _rabbitMqSettings.Filas[fila].RoutingKey, basicProperties: null, body: body);

            
            if (channel.WaitForConfirms(TimeSpan.FromSeconds(5))) 
            {
                _logger.LogInformation($"Mensagem confirmada na fila {fila}");
            }
            else
            {                
                _logger.LogError($"A mensagem não foi confirmada na fila {fila}");
            }

        }
    }
}
