using MediatR;

namespace JiraFake.Domain.Events.Models.SubTarefa
{
    public class SubTarefaEventHandler : INotificationHandler<AdicionarSubTarefaEvent>
    {
        public Task Handle(AdicionarSubTarefaEvent notification, CancellationToken cancellationToken)
        {
            //enviar para fila
            return Task.CompletedTask;
        }
    }
}
