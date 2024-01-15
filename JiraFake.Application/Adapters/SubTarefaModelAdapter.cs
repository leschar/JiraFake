using JiraFake.Application.ViewModels;
using JiraFake.Domain.Commands.SubTarefa;
using JiraFake.Domain.Models;
using JiraFake.Domain.Utils;

namespace JiraFake.Application.Adapters
{
    public static class SubTarefaModelAdapter
    {
        public static AdicionarSubTarefaCommand ConvertToDomain(AdicionarSubTarefaViewModel model)
        {
            return new AdicionarSubTarefaCommand(model.Nome, model.Descricao, model.TarefaId);
        }

        public static IEnumerable<ResponseSubTarefaViewModel> ConvertToView(IEnumerable<SubTarefa> model)
        {
            var tarefas = new List<ResponseSubTarefaViewModel>();

            foreach (SubTarefa tarefa in model)
            {
                ResponseSubTarefaViewModel response = new();
                response.Id = tarefa.Id;
                response.Nome = tarefa.Nome;
                response.Descricao = tarefa.Descricao;
                response.DataCadastro = tarefa.DataCadastro;
                response.Status = EnumUtils.ObterDescricaoEnum(tarefa.Status);
                tarefas.Add(response);
            }
            return tarefas;
        }
    }
}
