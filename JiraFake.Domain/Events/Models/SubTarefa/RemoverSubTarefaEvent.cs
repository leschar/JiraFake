using JiraFake.Domain.Messages.Events;

namespace JiraFake.Domain.Events.Models.Tarefa
{
    public class RemoverSubTarefaEvent : Event
    {
        public RemoverSubTarefaEvent(Guid id, bool ativo, Guid tarefaId)
        {
            Id = id;
            Ativo = ativo;
            TarefaId = tarefaId;
        }

        public Guid Id { get; private set; }
        public Guid TarefaId { get; private set; }
        public bool Ativo { get; private set; }
    }
}
