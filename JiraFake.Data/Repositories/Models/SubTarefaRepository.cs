using JiraFake.Data.Context;
using JiraFake.Domain.Interfaces.Models;
using JiraFake.Domain.Models;

namespace JiraFake.Data.Repositories.Models
{
    public class SubTarefaRepository : Repository<SubTarefa>, ISubTarefaRepository
    {
        public SubTarefaRepository(JiraFakeContext context) : base(context) { }
    }
}
