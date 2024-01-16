using JiraFake.Application.ViewModels;
using JiraFake.Domain.Commands.Tarefa;
using JiraFake.Domain.Models;
using JiraFake.Domain.Utils;

namespace JiraFake.Application.Adapters
{
    public static class TarefaModelAdapter
    {
        public static AdicionarTarefaCommand ConvertToDomain(AdicionarTarefaViewModel model)
        {
            return new AdicionarTarefaCommand(model.Nome, model.Descricao);
        }

        public static ResponseTarefaViewModel ConvertToView(Tarefa model)
        {
                ResponseTarefaViewModel response = new();
                response.Id = model.Id;
                response.Nome = model.Nome;
                response.Descricao = model.Descricao;
                response.DataCadastro = model.DataCadastro;
                response.Status = EnumUtils.ObterDescricaoEnum(model.Status);

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
                response.Status = EnumUtils.ObterDescricaoEnum(tarefa.Status);
                tarefas.Add(response);
            }
            return tarefas;
        }

        public static ResponseTarefaSubtarefaViewModel ConvertListToView(Tarefa model)
        {
            var tarefas = new ResponseTarefaSubtarefaViewModel
            {
                Id = model.Id,
                Nome = model.Nome,
                Descricao = model.Descricao,
                DataCadastro = model.DataCadastro,
                Status = EnumUtils.ObterDescricaoEnum(model.Status),

                SubTarefas = model.SubTarefas?.Select(SubTarefaModelAdapter.ConvertToView).ToList()
            };

            return tarefas;
        }
    }
}
