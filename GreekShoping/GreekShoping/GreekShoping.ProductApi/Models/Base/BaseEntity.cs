using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GreekShoping.ProductApi.Models.Base;

public class BaseEntity
{
    [Key]
    [Column("id")]
    public long Id {get; set;}
}
