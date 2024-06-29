namespace GreekShoping.OrderAPI.Messeges;

public class CartDetailVO
{
    public long Id { get; set; }
    public long CartHeaderId { get; set; }
    public long ProductId { get; set; }
    public virtual ProductVO Product { get; set; }
    public int count { get; set; }
}
