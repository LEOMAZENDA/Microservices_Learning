using GreekShoping.MessageBus.Services._MessageService;

namespace GreekShoping.OrderAPI.Messeges;

public class PaymentVO : BaseMessage
{
    public long OrderId { get; set; }
    public string Name { get; set; }
    public string CardNumber { get; set; }
    public string CVV { get; set; }
    public string ExpiryMonthYear { get; set; }
    public decimal PurchaseAmount { get; set; }
    public string email { get; set; }
}
