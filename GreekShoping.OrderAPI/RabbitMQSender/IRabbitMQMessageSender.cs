using GreekShoping.MessageBus;

namespace GreekShoping.OrderAPI.RabbitMQSender;

public interface IRabbitMQMessageSender
{
    void SendeMessage(BaseMessage baseMessage, string queueName);
}
