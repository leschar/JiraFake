using JiraFake.Application.ViewModels;
using JiraFake.Domain.Commands.Tarefa;
using JiraFake.Domain.Models;

namespace JiraFake.Application.Adapters
{
    public static class TarefaModelAdapter
    {
        public static AdicionarTarefaCommand ConvertToDomain(AdicionarTarefaViewModel model)
        {
            return new AdicionarTarefaCommand(model.Nome, model.Descricao);
        }

        public static EditarTarefaCommand ConvertToDomain(EditarTarefaViewModel model)
        {
            return new EditarTarefaCommand(model.Id, model.Nome, model.Descricao, model.Status);
        }

        public static ResponseTarefaViewModel? ConvertToView(Tarefa model)
        {
            ResponseTarefaViewModel response = new();
            if (model is null) return null;

            response.Id = model.Id;
            response.Nome = model.Nome;
            response.Descricao = model.Descricao;
            response.DataCadastro = model.DataCadastro;
            response.Status = model.Status;

            return response;
        }
        public static IEnumerable<ResponseTarefaViewModel> ConvertToView(IEnumerable<Tarefa> model)
        {
            var tarefas = new List<ResponseTarefaViewModel>();
            foreach (Tarefa tarefa in model)
            {
                ResponseTarefaViewModel response = new();
                response.Id = tarefa.Id;
                response.Nome = tarefa.Nome;
                response.Descricao = tarefa.Descricao;
                response.DataCadastro = tarefa.DataCadastro;
                response.Status = tarefa.Status;
                tarefas.Add(response);
            }
            return tarefas;
        }

        public static ResponseTarefaSubtarefaViewModel? ConvertListToView(Tarefa model)
        {
            ResponseTarefaSubtarefaViewModel tarefas = new();
            if (model is null) return null;
            tarefas.Id = model.Id;
            tarefas.Nome = model.Nome;
            tarefas.Descricao = model.Descricao;
            tarefas.DataCadastro = model.DataCadastro;
            tarefas.Status = model.Status;

            tarefas.SubTarefas = model.SubTarefas?.Select(SubTarefaModelAdapter.ConvertToView).ToList();

            return tarefas;
        }
    }
}
