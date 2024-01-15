using JiraFake.Domain.DomainObjects;
using JiraFake.Domain.Enum;

namespace JiraFake.Domain.Models
{
    public class SubTarefa : Entity
    {
        public SubTarefa() { }
        public SubTarefa(string nome, string subNome, Guid tarefaId)
        {
            Nome = nome;
            Descricao = subNome;
            TarefaId = tarefaId;
            DataCadastro = DateTime.Now;
            Status = StatusEnum.Aberto;
        }

        public Guid TarefaId { get;private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        public Tarefa Tarefa { get; set; }
    }
}
