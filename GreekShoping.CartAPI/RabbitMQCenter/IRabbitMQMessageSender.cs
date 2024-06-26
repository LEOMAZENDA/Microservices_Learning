using GreekShoping.MessageBus.Services._MessageService;

namespace GreekShoping.CartAPI.RabbitMQCenter;

public interface IRabbitMQMessageSender
{
    void SendeMessage(BaseMessage baseMessage, string queueName);
}
