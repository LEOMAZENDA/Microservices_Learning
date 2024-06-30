using GreekShoping.OrderAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreekShoping.OrderAPI.Models;

[Table("TbOrderHeader")]
public class OrderHeader : BaseEntity
{
    [Column("User_id")]
    public string UserId { get; set; }

    [Column("Coupon_code")]
    public string CouponCode { get; set; }

    [Column("Purchase_amount")]
    public decimal PurchaseAmount { get; set; }

    [Column("Discount_amount")]
    public decimal DiscountAmount { get; set; }

    [Column("First_name")]
    public string FirstName { get; set; }

    [Column("Last_name")]
    public string LastName { get; set; }

    [Column("Purchase_date")]
    public DateTime DateTime { get; set; }

    [Column("Order_time")]
    public DateTime OrderTime { get; set; }

    [Column("Phone_number")]
    public string Phone { get; set; }

    [Column("Email")]
    public string Email { get; set; }

    [Column("Card_number")]
    public string CardNumber { get; set; }

    [Column("CVV")]
    public string CVV { get; set; }

    [Column("Expiry_month_year")]
    public string ExpiryMonthYear { get; set; }

    [Column("Total_itens")]
    public int CartTotalItens { get; set; }

    public List<OrderDetail> OrderDetails { get; set; }

    [Column("Payment_status")]
    public bool PaymentStatus { get; set; }
}
