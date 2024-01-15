using JiraFake.Data.Context;
using JiraFake.Domain.Interfaces.Models;
using JiraFake.Domain.Models;

namespace JiraFake.Data.Repositories.Models
{
    public class TarefaRepository : Repository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(JiraFakeContext context) : base(context) { }
    }
}
