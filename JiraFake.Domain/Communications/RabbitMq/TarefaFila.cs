using JiraFake.Domain.Interfaces.Rabbit;
using Microsoft.Extensions.Logging;

namespace JiraFake.Domain.Communications.RabbitMq
{
    public class TarefaFila : IFilaRabbit
    {
        private readonly ILogger _logger;
        public TarefaFila(ILogger<TarefaFila> logger)
        {
            _logger = logger;
        }

        public void Processar(string mensagem)
        {

            _logger.LogInformation($"Processando a fila tarefa: {mensagem}");
            //fazer algo após receber da fila
        }
    }
}
