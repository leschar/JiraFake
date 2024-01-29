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
                .Include(x => x.SubTarefas
                               .Where(s => s.Ativo)
                               .OrderBy(s => s.Status)
                               .ThenBy(s => s.DataCadastro))
                .FirstOrDefaultAsync(t => t.Id == id && t.Ativo);
        }

        public async Task DesativarTarefa(Guid id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            tarefa.Ativo = false;
            _context.Tarefas.Update(tarefa);
        }
    }
}
