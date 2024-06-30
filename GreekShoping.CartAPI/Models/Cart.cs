namespace GreekShoping.OrderAPI.Models;

public class Cart
{
    public CartHeader CartHeader { get; set; }
    public IEnumerable<CartDetail> CartDetails { get; set; }

}
