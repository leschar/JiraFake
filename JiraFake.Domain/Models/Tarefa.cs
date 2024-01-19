using JiraFake.Domain.DomainObjects;
using JiraFake.Domain.Enum;

namespace JiraFake.Domain.Models
{
    public class Tarefa : Entity
    {
        public Tarefa() { }

        //Novo
        public Tarefa(string nome, string subNome)
        {
            Nome = nome;
            Descricao = subNome;
            DataCadastro = DateTime.Now;
            Status = StatusEnum.Aberto;
            Ativo = true;
        }

        //Editar
        public Tarefa(Guid id, string nome, string subNome, StatusEnum status)
        {
            Id = id;
            Nome = nome;
            Descricao = subNome;
            DataAtualizacao = DateTime.Now;
            Status = status;
            Ativo = true;
        }

        //Remover
        public Tarefa(Guid id)
        {
            Id=id;
            Ativo = false;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        public ICollection<SubTarefa> SubTarefas { get; set; }
    }
}
