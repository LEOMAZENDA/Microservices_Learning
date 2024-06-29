namespace GreekShoping.OrderAPI.Messeges
{
    public class UpdatePaymantResultVO
    {
        public long OrderId { get; set; }
        public string email { get; set; }
        public bool Status { get; set; }
    }
}