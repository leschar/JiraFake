using JiraFake.Application.ViewModels;
using JiraFake.Domain.Commands.Tarefa;

namespace JiraFake.Application.Adapters
{
    public static class TarefaModelAdapter
    {
        public static AdicionarTarefaCommand ConvertToDomain(AdicionarTarefaViewModel model)
        {
            return new AdicionarTarefaCommand(model.Nome, model.Descricao);
        }
    }
}
