using JiraFake.Domain.Enum;
using JiraFake.Domain.Messages.Events;

namespace JiraFake.Domain.Events.Models.SubTarefa
{
    public class AdicionarSubTarefaEvent : Event
    {
        public AdicionarSubTarefaEvent(Guid id, string nome, string descricao, DateTime dataCadastro, StatusEnum status)
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
        public StatusEnum Status { get; private set; }

    }
}
