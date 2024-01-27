using JiraFake.Data.Context;
using JiraFake.Domain.Interfaces.Models;
using JiraFake.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace JiraFake.Data.Repositories.Models
{
    public class SubTarefaRepository : Repository<SubTarefa>, ISubTarefaRepository
    {
        public SubTarefaRepository(JiraFakeContext context) : base(context) { }

        public async Task<SubTarefa> ObterSubTarefasPorId(Guid id)
        {
            return await _context.SubTarefas
                .FirstOrDefaultAsync(t => t.Id == id && t.Ativo);
        }

        public async Task DesativarSubTarefa(Guid id)
        {
            var subTarefa = await _context.SubTarefas.FindAsync(id);
            subTarefa.Ativo = false;
            _context.SubTarefas.Update(subTarefa);
        }
    }
}
