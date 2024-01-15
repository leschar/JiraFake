using FluentValidation.Results;
using JiraFake.Domain.Events.Models.Tarefa;
using JiraFake.Domain.Interfaces.Models;
using JiraFake.Domain.Messages.Commands;
using MediatR;

namespace JiraFake.Domain.Commands.Tarefa
{
    public class TarefaCommandHandler : CommandHandler,
                                        IRequestHandler<AdicionarTarefaCommand, ValidationResult>
    {
        private readonly ITarefaRepository _repository;
        public TarefaCommandHandler(ITarefaRepository repository)
        {
            _repository = repository;
        }
        public async Task<ValidationResult> Handle(AdicionarTarefaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;

            var tarefa = new Models.Tarefa(request.Nome, request.Descricao);

            await _repository.Add(tarefa);

            tarefa.AdicionarEvento(new AdicionarTarefaEvent(tarefa.Id, tarefa.Nome, tarefa.Descricao, tarefa.DataCadastro, tarefa.Status));

            return await PersistirDados(_repository.UnitOfWork);
        }
    }
}
