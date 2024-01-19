using JiraFake.Domain.Communications.RabbitMq;
using JiraFake.Domain.Enum;
using JiraFake.Domain.Events.Models.Tarefa;
using JiraFake.Domain.Utils;
using MediatR;

namespace JiraFake.Domain.Events.Models.SubTarefa
{
    public class SubTarefaEventHandler : INotificationHandler<AdicionarSubTarefaEvent>,
                                         INotificationHandler<EditarSubTarefaEvent>,
                                         INotificationHandler<RemoverSubTarefaEvent>
    {
        private readonly RabbitMqSender<AdicionarSubTarefaEvent> _rabbitAddMqSender;
        private readonly RabbitMqSender<EditarSubTarefaEvent> _rabbitEditMqSender;
        private readonly RabbitMqSender<RemoverSubTarefaEvent> _rabbitRemoveMqSender;

        public SubTarefaEventHandler(RabbitMqSender<AdicionarSubTarefaEvent> rabbitMqSender,
                                         RabbitMqSender<EditarSubTarefaEvent> rabbitEditMqSender,
                                         RabbitMqSender<RemoverSubTarefaEvent> rabbitRemoveMqSender)
        {
            _rabbitAddMqSender = rabbitMqSender;
            _rabbitEditMqSender = rabbitEditMqSender;
            _rabbitRemoveMqSender = rabbitRemoveMqSender;
        }

        public async Task Handle(AdicionarSubTarefaEvent notification, CancellationToken cancellationToken)
        {
            // Usar a instância já injetada no construtor
            await _rabbitAddMqSender.SendMessageAsync(notification, EnumUtils.ObterDescricaoEnum(FilaRabbitMqEnum.SubTarefa));
        }

        public async Task Handle(EditarSubTarefaEvent notification, CancellationToken cancellationToken)
        {
            await _rabbitEditMqSender.SendMessageAsync(notification, EnumUtils.ObterDescricaoEnum(FilaRabbitMqEnum.Tarefa));
        }
        public async Task Handle(RemoverSubTarefaEvent notification, CancellationToken cancellationToken)
        {
            await _rabbitRemoveMqSender.SendMessageAsync(notification, EnumUtils.ObterDescricaoEnum(FilaRabbitMqEnum.Tarefa));
        }
    }
}
