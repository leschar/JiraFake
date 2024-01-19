using JiraFake.Domain.Messages.Events;

namespace JiraFake.Domain.Events.Models.Tarefa
{
    public class RemoverTarefaEvent : Event
    {
        public RemoverTarefaEvent(Guid id, bool ativo)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public bool Ativo { get; private set; }
    }
}
