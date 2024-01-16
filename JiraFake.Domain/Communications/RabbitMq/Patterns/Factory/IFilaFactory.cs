using JiraFake.Domain.Interfaces.Rabbit;

namespace JiraFake.Domain.Communications.RabbitMq.Patterns.Factory
{
    public interface IFilaFactory
    {
        IFilaRabbit CriarFila(string texto);
    }
}
