using JiraFake.Domain.Communications.RabbitMq;
using JiraFake.Domain.Enum;
using JiraFake.Domain.Utils;
using MediatR;

namespace JiraFake.Domain.Events.Models.SubTarefa
{
    public class SubTarefaEventHandler : INotificationHandler<AdicionarSubTarefaEvent>
    {
        private readonly RabbitMqSender<AdicionarSubTarefaEvent> _rabbitMqSender;

        public SubTarefaEventHandler(RabbitMqSender<AdicionarSubTarefaEvent> rabbitMqSender)
        {
            _rabbitMqSender = rabbitMqSender;
        }

        public async Task Handle(AdicionarSubTarefaEvent notification, CancellationToken cancellationToken)
        {
            // Usar a instância já injetada no construtor
            await _rabbitMqSender.SendMessageAsync(notification, EnumUtils.ObterDescricaoEnum(FilaRabbitMqEnum.SubTarefa));
        }
    }
}
