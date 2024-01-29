using JiraFake.Application.ViewModels;
using JiraFake.Domain.Commands.SubTarefa;
using JiraFake.Domain.Commands.Tarefa;
using JiraFake.Domain.Enum;
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

        public static EditarSubTarefaCommand ConvertToDomain(EditarSubTarefaViewModel model)
        {
            return new EditarSubTarefaCommand(model.Id, model.TarefaId, model.Nome, model.Descricao, model.Status);
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
                response.Status = tarefa.Status;
                tarefas.Add(response);
            }
            return tarefas;
        }

        public static ResponseSubTarefaViewModel ConvertToView(SubTarefa model)
        {
            ResponseSubTarefaViewModel response = new();
            response.Id = model.Id;
            response.TarefaId = model.TarefaId;
            response.Nome = model.Nome;
            response.Descricao = model.Descricao;
            response.DataCadastro = model.DataCadastro;
            response.Status = model.Status;

            return response;
        }
    }
}
