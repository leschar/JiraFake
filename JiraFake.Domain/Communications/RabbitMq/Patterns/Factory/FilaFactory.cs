using JiraFake.Domain.Enum;
using JiraFake.Domain.Interfaces.Rabbit;
using JiraFake.Domain.Utils;
using Microsoft.Extensions.Logging;

namespace JiraFake.Domain.Communications.RabbitMq.Patterns.Factory
{
    public class FilaFactory : IFilaFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        public FilaFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public IFilaRabbit CriarFila(string texto)
        {
            switch (texto)
            {
                case var tarefaText when tarefaText == EnumUtils.ObterDescricaoEnum(FilaRabbitMqEnum.Tarefa):
                    return new TarefaFila(_loggerFactory.CreateLogger<TarefaFila>());

                case var subTarefaText when subTarefaText == EnumUtils.ObterDescricaoEnum(FilaRabbitMqEnum.SubTarefa):
                    return new SubTarefaFila(_loggerFactory.CreateLogger<SubTarefaFila>());

                default:
                    throw new ArgumentException("Fila não identificada");
            }
        }
    }
}
