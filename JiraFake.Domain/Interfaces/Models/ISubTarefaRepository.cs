using JiraFake.Domain.Interfaces.Infra;
using JiraFake.Domain.Models;

namespace JiraFake.Domain.Interfaces.Models
{
    public interface ISubTarefaRepository : IRepository<SubTarefa>
    {

        Task DesativarSubTarefa(Guid id);
    }
}
