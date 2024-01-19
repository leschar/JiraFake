using FluentValidation;
using JiraFake.Domain.Enum;
using JiraFake.Domain.Messages.Commands;

namespace JiraFake.Domain.Commands.Tarefa
{
    public class EditarTarefaCommand : Command
    {
        public EditarTarefaCommand(Guid id, string nome, string descricao,StatusEnum status)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Status = status;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public StatusEnum Status { get; set; }
        public bool TarefaExiste { get; set; }

        public void ValidarTarefa(bool valido)
        {
            TarefaExiste = valido;
        }

        public override bool EhValido()
        {
            ValidationResult = new EditarTarefaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    

    public class EditarTarefaValidation : AbstractValidator<EditarTarefaCommand>
    {
        public EditarTarefaValidation()
        {
            RuleFor(i => i.Nome)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome não pode ser nulo.");

            RuleFor(i => i.Nome)
               .Length(2, 50)
               .WithMessage("O Nome deve ter entre 2 e 50 caracteres.");

            RuleFor(x => x.Descricao)
             .Cascade(CascadeMode.StopOnFirstFailure)
             .Must(value => string.IsNullOrWhiteSpace(value) || value.Length <= 500)
                 .WithMessage("Descricção deve estar vazio ou ter no máximo 500 caracteres.");

            RuleFor(x => x.TarefaExiste)
               .NotNull()
               .NotEmpty()
               .WithMessage("Tarefa não existe.");
        }
    }
}
