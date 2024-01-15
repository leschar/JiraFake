using JiraFake.Domain.Messages.Events;

namespace JiraFake.Domain.Events.Models.Tarefa
{
    public class AdicionarTarefaEvent : Event
    {
        public AdicionarTarefaEvent(Guid id, string nome, string descricao, DateTime dataCadastro, bool status)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            DataCadastro = dataCadastro;
            Status = status;
        }

        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Status { get; private set; }

    }
}
