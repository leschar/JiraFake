using FluentValidation.Results;
using JiraFake.Domain.Commands.SubTarefa;
using JiraFake.Domain.Events.Models.SubTarefa;
using JiraFake.Domain.Interfaces.Models;
using JiraFake.Domain.Messages.Commands;
using MediatR;

namespace JiraFake.Domain.Commands.SubSubTarefa
{
    public class SubTarefaCommandHandler : CommandHandler,
                                        IRequestHandler<AdicionarSubTarefaCommand, ValidationResult>
    {
        private readonly ISubTarefaRepository _repository;
        public SubTarefaCommandHandler(ISubTarefaRepository repository)
        {
            _repository = repository;
        }
        public async Task<ValidationResult> Handle(AdicionarSubTarefaCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido()) return request.ValidationResult;

            //TODO: Validar tarefa existe

            var tarefa = new Domain.Models.SubTarefa(request.Nome, request.Descricao, request.TarefaId);

            await _repository.Add(tarefa);

            tarefa.AdicionarEvento(new AdicionarSubTarefaEvent(tarefa.Id, tarefa.Nome, tarefa.Descricao, tarefa.DataCadastro, tarefa.Status));

            return await PersistirDados(_repository.UnitOfWork);
        }
    }
}
