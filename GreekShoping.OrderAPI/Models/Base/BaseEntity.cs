using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GreekShoping.OrderAPI.Models.Base;

public class BaseEntity
{
    [Key]
    [Column("id")]
    public long Id {get; set;}
}
