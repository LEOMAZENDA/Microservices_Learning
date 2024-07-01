using GreekShoping.MessageBus;

namespace GreekShoping.PaymentAPI.RabbitMQSender;

public interface IRabbitMQMessageSender
{
    void SendeMessage(BaseMessage baseMessage);
}