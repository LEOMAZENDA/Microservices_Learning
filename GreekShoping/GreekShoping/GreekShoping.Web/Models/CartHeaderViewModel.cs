namespace GreekShoping.Web.Models;

public class CartHeaderViewModel
{
    public long Id { get; set; }
    public string UserId { get; set; }
    public string CouponCode { get; set; }
    public double PurchaswAmount { get; set; } // total da compra
}
