namespace GreekShoping.Web.Models;

public class CartDetailViewModel
{
    public long Id { get; set; }
    public long CartHeaderId { get; set; }
    public long ProductId { get; set; }
    public ProductViewModel Product { get; set; }
    public CartHeaderViewModel CartHeader { get; set; }
    public int count { get; set; }
}
