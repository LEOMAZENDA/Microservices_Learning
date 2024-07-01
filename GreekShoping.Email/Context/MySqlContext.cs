using GreekShoping.Email.Models;
using Microsoft.EntityFrameworkCore;

namespace GreekShoping.Email.Context;

public class MySqlContext : DbContext
{
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

    public DbSet<EmailLog> Emails { get; set; }
}
