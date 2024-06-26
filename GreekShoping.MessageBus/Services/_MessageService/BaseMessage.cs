namespace GreekShoping.MessageBus.Services._MessageService;

public class BaseMessage
{
    public long Id { get; set; }
    public DateTime MessageCreated { get; set; }
}
