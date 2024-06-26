namespace GreekShoping.MessageBus.Services._MessageService;
public interface IMessageBus
{
    Task PublicMessage(BaseMessage message, string queueName);
}
