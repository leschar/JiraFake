using JiraFake.Domain.Enum;

namespace JiraFake.Application.ViewModels
{
    public class ResponseTarefaViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Status { get; set; }
    }
}
