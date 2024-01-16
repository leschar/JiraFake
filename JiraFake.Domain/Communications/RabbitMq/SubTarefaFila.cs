using JiraFake.Domain.Interfaces.Rabbit;
using Microsoft.Extensions.Logging;

namespace JiraFake.Domain.Communications.RabbitMq
{
    public class SubTarefaFila : IFilaRabbit
    {
        private readonly ILogger _logger;
        public SubTarefaFila(ILogger<SubTarefaFila> logger)
        {
            _logger = logger;
        }
        public void Processar(string mensagem)
        {

            _logger.LogInformation($"Processando a fila sub-tarefa: {mensagem}");
            //fazer algo após receber da fila
        }
    }
}
