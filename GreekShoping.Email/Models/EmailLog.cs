using GreekShoping.Email.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreekShoping.Email.Models;

[Table("TbEmailLog")]
public class EmailLog : BaseEntity
{
    [Column("Email")]
    public string Email { get; set; }

    [Column("Log")]
    public string Log { get; set; }

    [Column("SentDate")]
    public DateTime SentDate { get; set; }
}
