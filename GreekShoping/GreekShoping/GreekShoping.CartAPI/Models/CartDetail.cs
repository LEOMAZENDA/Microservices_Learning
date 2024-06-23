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

    public virtual Product Product { get; set; }
    public virtual CartHeader CartHeader { get; set; }

    [Column("Count")]
    public int Count { get; set; }
}
