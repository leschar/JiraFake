using FluentValidation;
using JiraFake.Domain.Messages.Commands;

namespace JiraFake.Domain.Commands.Tarefa
{
    public class AdicionarTarefaCommand : Command
    {
        public AdicionarTarefaCommand(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarTarefaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarTarefaValidation : AbstractValidator<AdicionarTarefaCommand>
    {
        public AdicionarTarefaValidation()
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
        }
    }
}
