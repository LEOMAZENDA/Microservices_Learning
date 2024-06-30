namespace GreekShoping.MessageBus;
public interface IMessageBus
{
    Task PublicMessage(BaseMessage message, string queueName);
}
