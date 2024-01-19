using JiraFake.Domain.DomainObjects;
using JiraFake.Domain.Enum;

namespace JiraFake.Domain.Models
{
    public class SubTarefa : Entity
    {
        public SubTarefa() { }

        //Novo
        public SubTarefa(string nome, string subNome, Guid tarefaId)
        {
            Nome = nome;
            Descricao = subNome;
            TarefaId = tarefaId;
            DataCadastro = DateTime.Now;
            Status = StatusEnum.Aberto;
            Ativo = true;
        }

        //Editar
        public SubTarefa(Guid id,Guid tarefaId, string nome, string subNome, StatusEnum status)
        {
            Id = id;
            TarefaId = tarefaId;
            Nome = nome;
            Descricao = subNome;
            DataAtualizacao = DateTime.Now;
            Status = status;
            Ativo = true;
        }
        //Remover
        public SubTarefa(Guid id)
        {
            Id = id;
            Ativo = false;
        }

        public Guid TarefaId { get;private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        public Tarefa Tarefa { get; set; }
    }
}
