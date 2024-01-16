using JiraFake.Domain.Interfaces.Infra;
using JiraFake.Domain.Models;

namespace JiraFake.Domain.Interfaces.Models
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        Task<Tarefa> ObterTarefasPorId(Guid id);
    }
}
