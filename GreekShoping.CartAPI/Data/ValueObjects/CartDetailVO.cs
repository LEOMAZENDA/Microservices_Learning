namespace GreekShoping.OrderAPI.Data.ValueObjects;

public class CartDetailVO
{
    public long Id { get; set; }
    public long CartHeaderId { get; set; }
    public long ProductId { get; set; }
    public ProductVO Product { get; set; }
    public CartHeaderVO CartHeader { get; set; }
    public int count { get; set; }
}
