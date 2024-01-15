using FluentValidation;
using JiraFake.Domain.Messages.Commands;

namespace JiraFake.Domain.Commands.SubTarefa
{
    public class AdicionarSubTarefaCommand : Command
    {
        public AdicionarSubTarefaCommand(string nome, string descricao, Guid tarefaId)
        {
            Nome = nome;
            Descricao = descricao;
            TarefaId = tarefaId;
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Guid TarefaId { get; set; }

        public override bool EhValido()
        {
            ValidationResult = new AdicionarSubTarefaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AdicionarSubTarefaValidation : AbstractValidator<AdicionarSubTarefaCommand>
    {
        public AdicionarSubTarefaValidation()
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
