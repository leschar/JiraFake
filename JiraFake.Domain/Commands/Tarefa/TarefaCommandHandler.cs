using FluentValidation.Results;
using JiraFake.Domain.Events.Models.Tarefa;
using JiraFake.Domain.Interfaces.Models;
using JiraFake.Domain.Messages.Commands;
using MediatR;

namespace JiraFake.Domain.Commands.Tarefa
{
    public class TarefaCommandHandler : CommandHandler,
                                        IRequestHandler<AdicionarTarefaCommand, ValidationResult>,
                                        IRequestHandler<EditarTarefaCommand, ValidationResult>,
                                        IRequestHandler<RemoverTarefaCommand, ValidationResult>
    {
        private readonly ITarefaRepository _repository;
        private readonly ISubTarefaRepository _repositorySub;
        public TarefaCommandHandler(ITarefaRepository repository, ISubTarefaRepository repositorySub)
        {
            _repository = repository;
            _repositorySub = repositorySub;
        }
        public async Task<ValidationResult> Handle(AdicionarTarefaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;

            var tarefa = new Models.Tarefa(request.Nome, request.Descricao);

            await _repository.Add(tarefa);

            tarefa.AdicionarEvento(new AdicionarTarefaEvent(tarefa.Id, tarefa.Nome, tarefa.Descricao, tarefa.DataCadastro, tarefa.Status));

            return await PersistirDados(_repository.UnitOfWork);
        }
        public async Task<ValidationResult> Handle(EditarTarefaCommand request, CancellationToken cancellationToken)
        {
            var existeTarefa = await _repository.GetById(request.Id);
            request.ValidarTarefa(existeTarefa is not null);

            if (!request.EhValido()) return request.ValidationResult;

            var tarefa = new Models.Tarefa(request.Id,request.Nome, request.Descricao, request.Status);

            _repository.Update(tarefa);

            tarefa.AdicionarEvento(new EditarTarefaEvent(tarefa.Id, tarefa.Nome, tarefa.Descricao, tarefa.DataCadastro, tarefa.Status, tarefa.Ativo));

            return await PersistirDados(_repository.UnitOfWork);
        }
        public async Task<ValidationResult> Handle(RemoverTarefaCommand request, CancellationToken cancellationToken)
        {
            var existeTarefa = await _repository.GetById(request.Id);
            request.ValidarTarefa(existeTarefa is not null);
            //validar se existe sub tarefas ativas
            var existeSubTarefaAtiva = await _repositorySub.Find(c=>c.Ativo && c.TarefaId == request.Id);
            request.ValidarSubTarefaAtiva(existeSubTarefaAtiva.Any());

            if (!request.EhValido()) return request.ValidationResult;

            var tarefa = new Models.Tarefa(request.Id);

            await _repository.DesativarTarefa(request.Id);

            tarefa.AdicionarEvento(new RemoverTarefaEvent(tarefa.Id , tarefa.Ativo));

            return await PersistirDados(_repository.UnitOfWork);
        }
    }
}
