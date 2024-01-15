using JiraFake.Domain.Interfaces.Rabbit;

namespace JiraFake.Domain.Communications.RabbitMq
{
    public class SubTarefaFila : IFilaRabbit
    {
        public void Processar(string mensagem)
        {
            //fazer algo após receber da fila
        }
    }
}
