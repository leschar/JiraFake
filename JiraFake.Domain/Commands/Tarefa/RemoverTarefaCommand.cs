using FluentValidation;
using JiraFake.Domain.Messages.Commands;

namespace JiraFake.Domain.Commands.Tarefa
{
    public class RemoverTarefaCommand : Command
    {
        public RemoverTarefaCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
        public bool TarefaExiste { get; set; }
        public bool SubTarefaAtiva { get; set; }

        public void ValidarTarefa(bool valido)
        {
            TarefaExiste = valido;
        }
        public void ValidarSubTarefaAtiva(bool valido)
        {
            SubTarefaAtiva = valido;
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverTarefaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    public class RemoverTarefaValidation : AbstractValidator<RemoverTarefaCommand>
    {
        public RemoverTarefaValidation()
        {
            RuleFor(x => x.TarefaExiste)
                .NotNull()
                .NotEmpty()
                .WithMessage("Tarefa não existe.");

            RuleFor(x => x.SubTarefaAtiva)
                .Equal(false)
                .WithMessage("Existem sub tarefas ativas, primeiro remova para poder excluir esta tarefa.");
        }
    }
}
