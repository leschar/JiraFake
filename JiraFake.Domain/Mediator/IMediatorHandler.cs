using FluentValidation.Results;
using JiraFake.Domain.Messages.Commands;
using JiraFake.Domain.Messages.Events;

namespace JiraFake.Domain.Mediator
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task<ValidationResult> EnviarComando<T>(T comando) where T : Command;
    }
}
