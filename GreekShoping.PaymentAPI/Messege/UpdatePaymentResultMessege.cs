using GreekShoping.MessageBus;

namespace GreekShoping.PaymentAPI.Messege;

public class UpdatePaymentResultMessege : BaseMessage
{
    public long OrderId { get; set; }
    public bool Status { get; set; }
    public string Email { get; set; }
}
