using GreekShoping.CartAPI.Data.ValueObjects;

namespace GreekShoping.CartAPI.Message;

public class CheckouHeaderVO
{
    public long Id { get; set; }
    public string UserId { get; set; }
    public string CouponCode { get; set; }
    public decimal PurchaseAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateTime { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string CardNumber { get; set; }
    public string CVV { get; set; }
    public string ExpiryMothYear { get; set; }
    //public string ExpiryMothYear { get; set; }
    //public IEnumerable<CartDetailVO> ExpiryMothYear { get; set; }
  
            
}
