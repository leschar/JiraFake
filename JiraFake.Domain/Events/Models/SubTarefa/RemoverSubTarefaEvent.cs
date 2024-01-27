using JiraFake.Domain.Messages.Events;

namespace JiraFake.Domain.Events.Models.Tarefa
{
    public class RemoverSubTarefaEvent : Event
    {
        public RemoverSubTarefaEvent(Guid id, bool ativo)
        {
            Id = id;
            Ativo = ativo;
        }

        public Guid Id { get; private set; }
        public bool Ativo { get; private set; }
    }
}
