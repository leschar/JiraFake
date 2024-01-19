using JiraFake.Domain.Enum;
using JiraFake.Domain.Messages.Events;

namespace JiraFake.Domain.Events.Models.Tarefa
{
    public class EditarSubTarefaEvent : Event
    {
        public EditarSubTarefaEvent(Guid id, string nome, string descricao, DateTime dataAtualizacao, StatusEnum status, bool ativo)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            DataAtualizacao = dataAtualizacao;
            Status = status;
            Ativo = ativo;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataAtualizacao { get; private set; }
        public StatusEnum Status { get; private set; }
        public bool Ativo { get;private set; }

    }
}
