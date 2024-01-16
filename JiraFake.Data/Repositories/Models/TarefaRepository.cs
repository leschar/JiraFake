using JiraFake.Data.Context;
using JiraFake.Domain.Interfaces.Models;
using JiraFake.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JiraFake.Data.Repositories.Models
{
    public class TarefaRepository : Repository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(JiraFakeContext context) : base(context) { }

        public async Task<Tarefa> ObterTarefasPorId(Guid id)
        {
            return await _context.Tarefas
                .Include(x => x.SubTarefas)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
