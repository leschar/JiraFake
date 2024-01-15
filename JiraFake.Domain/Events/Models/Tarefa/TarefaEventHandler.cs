using MediatR;

namespace JiraFake.Domain.Events.Models.Tarefa
{
    public class TarefaEventHandler : INotificationHandler<AdicionarTarefaEvent>
    {
        public Task Handle(AdicionarTarefaEvent notification, CancellationToken cancellationToken)
        {
            //enviar para fila
            return Task.CompletedTask;
        }
    }
}
