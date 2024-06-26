using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GreekShoping.CouponAPI.Models.Base;

namespace GreekShoping.CouponAPI.Models;

[Table("TbCoupon")]
public class Coupon : BaseEntity
{
    [Required]
    [Column("coupon-code")]
    [StringLength(30)]
    public string CouponCode { get; set; }

    [Required]
    [Column("discount_amount")]
    public decimal DiscountAmount { get; set; }
}
