using FluentValidation;
using JiraFake.Domain.Messages.Commands;

namespace JiraFake.Domain.Commands.Tarefa
{
    public class RemoverSubTarefaCommand : Command
    {
        public RemoverSubTarefaCommand(Guid id, Guid tarefaId)
        {
            Id = id;
            TarefaId = tarefaId;
        }

        public Guid Id { get; set; }
        public Guid TarefaId { get; set; }
        public bool TarefaExiste { get; set; }
        public bool SubTarefaExiste { get; set; }

        public void ValidarTarefa(bool valido)
        {
            TarefaExiste = valido;
        }
        public void ValidarSubTarefa(bool valido)
        {
            SubTarefaExiste = valido;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverSubTarefaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class RemoverSubTarefaValidation : AbstractValidator<RemoverSubTarefaCommand>
    {
        public RemoverSubTarefaValidation()
        {
            RuleFor(x => x.TarefaExiste)
               .NotNull()
               .NotEmpty()
               .WithMessage("Tarefa não existe.");

            RuleFor(x => x.SubTarefaExiste)
               .NotNull()
               .NotEmpty()
               .WithMessage("Sub Tarefa não existe.")
               .When(x => x.TarefaExiste); ;
        }
    }
}
