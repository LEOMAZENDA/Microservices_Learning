using GreekShoping.OrderAPI.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreekShoping.OrderAPI.Models;

[Table("TbOrderDetail")]
public class OrderDetail :BaseEntity
{
    [ForeignKey("OrderHeaderId")]
    public long OrderHeaderId { get; set; }

    public virtual OrderHeader OrderHeader { get; set; }


    [Column("ProductId")]
    public long ProductId { get; set; }


    [Column("Count")]
    public int Count { get; set; }


    [Column("Product_name")]
    public string ProductName { get; set; }


    [Column("Price")]
    public decimal Price { get; set; }
}
