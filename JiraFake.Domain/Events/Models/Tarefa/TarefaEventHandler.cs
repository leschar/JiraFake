using JiraFake.Domain.Communications.RabbitMq;
using JiraFake.Domain.Enum;
using JiraFake.Domain.Utils;
using MediatR;

namespace JiraFake.Domain.Events.Models.Tarefa
{
    public class TarefaEventHandler : INotificationHandler<AdicionarTarefaEvent>
    {
        private readonly RabbitMqSender<AdicionarTarefaEvent> _rabbitMqSender;

        public TarefaEventHandler(RabbitMqSender<AdicionarTarefaEvent> rabbitMqSender)
        {
            _rabbitMqSender = rabbitMqSender;
        }

        public async Task Handle(AdicionarTarefaEvent notification, CancellationToken cancellationToken)
        {
            await _rabbitMqSender.SendMessageAsync(notification, EnumUtils.ObterDescricaoEnum(FilaRabbitMqEnum.Tarefa));
        }
    }

}
