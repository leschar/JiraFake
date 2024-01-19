using JiraFake.Domain.Communications.RabbitMq;
using JiraFake.Domain.Enum;
using JiraFake.Domain.Utils;
using MediatR;

namespace JiraFake.Domain.Events.Models.Tarefa
{
    public class TarefaEventHandler : INotificationHandler<AdicionarTarefaEvent>,
                                      INotificationHandler<EditarTarefaEvent>,
                                      INotificationHandler<RemoverTarefaEvent>
    {
        private readonly RabbitMqSender<AdicionarTarefaEvent> _rabbitAddMqSender;
        private readonly RabbitMqSender<EditarTarefaEvent> _rabbitEditMqSender;
        private readonly RabbitMqSender<RemoverTarefaEvent> _rabbitRemoveMqSender;

        public TarefaEventHandler(RabbitMqSender<AdicionarTarefaEvent> rabbitAddMqSender,
                                  RabbitMqSender<EditarTarefaEvent> rabbitEditMqSender,
                                  RabbitMqSender<RemoverTarefaEvent> rabbitRemoveMqSender)
        {
            _rabbitAddMqSender = rabbitAddMqSender;
            _rabbitEditMqSender = rabbitEditMqSender;
            _rabbitRemoveMqSender = rabbitRemoveMqSender;
        }

        public async Task Handle(AdicionarTarefaEvent notification, CancellationToken cancellationToken)
        {
            await _rabbitAddMqSender.SendMessageAsync(notification, EnumUtils.ObterDescricaoEnum(FilaRabbitMqEnum.Tarefa));
        }

        public async Task Handle(EditarTarefaEvent notification, CancellationToken cancellationToken)
        {
            await _rabbitEditMqSender.SendMessageAsync(notification, EnumUtils.ObterDescricaoEnum(FilaRabbitMqEnum.Tarefa));
        }
        public async Task Handle(RemoverTarefaEvent notification, CancellationToken cancellationToken)
        {
            await _rabbitRemoveMqSender.SendMessageAsync(notification, EnumUtils.ObterDescricaoEnum(FilaRabbitMqEnum.Tarefa));
        }
    }

}
