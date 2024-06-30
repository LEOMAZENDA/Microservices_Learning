using GreekShoping.MessageBus;

namespace GreekShoping.CartAPI.RabbitMQSender;

public interface IRabbitMQMessageSender
{
    void SendeMessage(BaseMessage baseMessage, string queueName);
}
