using JiraFake.Domain.Interfaces.Rabbit;

namespace JiraFake.Domain.Communications.RabbitMq
{
    public class TarefaFila : IFilaRabbit
    {
        public void Processar(string mensagem)
        {
            //fazer algo após receber da fila
        }
    }
}
