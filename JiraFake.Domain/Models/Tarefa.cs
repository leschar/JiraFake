using JiraFake.Domain.DomainObjects;

namespace JiraFake.Domain.Models
{
    public class Tarefa : Entity
    {
        public Tarefa() { }
        public Tarefa(string nome, string subNome)
        {
            Nome = nome;
            Descricao = subNome;
            DataCadastro = DateTime.Now;
            Status = Enum.StatusEnum.Aberto;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        public ICollection<SubTarefa> SubTarefas { get; set; }
    }
}
