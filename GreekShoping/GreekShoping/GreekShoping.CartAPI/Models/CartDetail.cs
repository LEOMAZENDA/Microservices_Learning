using GreekShoping.CartAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreekShoping.CartAPI.Models;

[Table("TbCartDetail")]
public class CartDetail :BaseEntity
{
    [ForeignKey("CartHeaderId")]
    public long CartHeaderId { get; set; }

    [ForeignKey("ProductId")]
    public long ProductId { get; set; }

    public Product Product { get; set; }
    public CartHeader CartHeader { get; set; }

    [Column("count")]
    public int count { get; set; }
}
