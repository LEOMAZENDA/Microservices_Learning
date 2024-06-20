using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GreekShoping.ProductApi.Models.Base;

namespace GreekShoping.ProductApi.Models;

[Table("TbProduct")]
public class Product : BaseEntity
{
    [Required]
    [Column("name")]
    [StringLength(150)]
    public string Name { get; set; }

    [Required]
    [Column("price")]
    [Range(1, 10000)]
    public decimal Price { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    [Column("category_name")]
    [StringLength(50)]
    public string CategoryName { get; set; }

    [Column("image_url")]
    [StringLength(300)]
    public string ImageUrl { get; set; }
}
