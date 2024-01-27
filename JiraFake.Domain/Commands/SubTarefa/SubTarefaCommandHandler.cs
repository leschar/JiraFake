using FluentValidation.Results;
using JiraFake.Domain.Commands.SubTarefa;
using JiraFake.Domain.Commands.Tarefa;
using JiraFake.Domain.Events.Models.SubTarefa;
using JiraFake.Domain.Events.Models.Tarefa;
using JiraFake.Domain.Interfaces.Models;
using JiraFake.Domain.Messages.Commands;
using MediatR;

namespace JiraFake.Domain.Commands.SubSubTarefa
{
    public class SubTarefaCommandHandler : CommandHandler,
                                        IRequestHandler<AdicionarSubTarefaCommand, ValidationResult>,
                                        IRequestHandler<EditarSubTarefaCommand, ValidationResult>,
                                        IRequestHandler<RemoverSubTarefaCommand, ValidationResult>
    {
        private readonly ISubTarefaRepository _repository;
        private readonly ITarefaRepository _repositoryTarefa;
        public SubTarefaCommandHandler(ISubTarefaRepository repository, ITarefaRepository repositoryTarefa)
        {
            _repository = repository;
            _repositoryTarefa = repositoryTarefa;
        }
        public async Task<ValidationResult> Handle(AdicionarSubTarefaCommand request, CancellationToken cancellationToken)
        {
            var existeTarefa = await _repositoryTarefa.GetById(request.TarefaId);
            request.ValidarTarefa(existeTarefa is not null);

            if (!request.EhValido()) return request.ValidationResult;

            var tarefa = new Models.SubTarefa(request.Nome, request.Descricao, request.TarefaId);
            

            await _repository.Add(tarefa);

            tarefa.AdicionarEvento(new AdicionarSubTarefaEvent(tarefa.Id, tarefa.Nome, tarefa.Descricao, tarefa.DataCadastro, tarefa.Status));

            return await PersistirDados(_repository.UnitOfWork);
        }
        public async Task<ValidationResult> Handle(EditarSubTarefaCommand request, CancellationToken cancellationToken)
        {
            var existeTarefa = await _repositoryTarefa.GetById(request.TarefaId);
            request.ValidarTarefa(existeTarefa is not null);

            var existeSubTarefa = await _repository.Find(c => c.Ativo && c.Id == request.Id && c.TarefaId == request.TarefaId);
            request.ValidarSubTarefa(existeSubTarefa.Any());
            //criar campo para informar o motivo da alteração

            if (!request.EhValido()) return request.ValidationResult;

            var subTarefa = new Models.SubTarefa(request.Id,request.TarefaId, request.Nome, request.Descricao, request.Status);

            _repository.Update(subTarefa);

            subTarefa.AdicionarEvento(new EditarSubTarefaEvent(subTarefa.Id, subTarefa.Nome, subTarefa.Descricao, subTarefa.DataCadastro, subTarefa.Status, subTarefa.Ativo));

            return await PersistirDados(_repository.UnitOfWork);
        }
        public async Task<ValidationResult> Handle(RemoverSubTarefaCommand request, CancellationToken cancellationToken)
        {
            var existeSubTarefa = await _repository.Find(c => c.Ativo && c.Id == request.Id && c.TarefaId == request.TarefaId);
            request.ValidarSubTarefa(existeSubTarefa.Any());

            //criar campo para informar o motivo da exclusao

            if (!request.EhValido()) return request.ValidationResult;

            var subTarefa = new Models.SubTarefa(request.Id);

            await _repository.DesativarSubTarefa(request.Id);

            subTarefa.AdicionarEvento(new RemoverSubTarefaEvent(subTarefa.Id, subTarefa.Ativo));

            return await PersistirDados(_repository.UnitOfWork);
        }
    }
}
