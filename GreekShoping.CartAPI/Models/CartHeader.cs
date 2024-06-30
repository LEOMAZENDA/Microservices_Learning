using GreekShoping.OrderAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreekShoping.OrderAPI.Models;

[Table("TbCartHeader")]
public class CartHeader: BaseEntity
{
    [Column("user_id")]
    public string UserId { get; set; }
    [Column("coupon_code")]
    public string CouponCode { get; set; }
}
