using JiraFake.Application.ViewModels;
using JiraFake.Domain.Commands.SubTarefa;

namespace JiraFake.Application.Adapters
{
    public static class SubTarefaModelAdapter
    {
        public static AdicionarSubTarefaCommand ConvertToDomain(AdicionarSubTarefaViewModel model)
        {
            return new AdicionarSubTarefaCommand(model.Nome, model.Descricao, model.TarefaId);
        }
    }
}
