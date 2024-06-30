using GreekShoping.MessageBus.Services._MessageService;

namespace GreekShoping.CartAPI.RabbitMQSenter;

public interface IRabbitMQMessageSender
{
    void SendeMessage(BaseMessage baseMessage, string queueName);
}
