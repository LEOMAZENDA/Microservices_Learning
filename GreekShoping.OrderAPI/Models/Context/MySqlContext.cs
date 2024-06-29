using Microsoft.EntityFrameworkCore;

namespace GreekShoping.OrderAPI.Models.Context;

public class MySqlContext : DbContext
{
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }


    public DbSet<OrderDetail> OrderDetail { get; set; }
    public DbSet<OrderHeader> OrderHeader { get; set; }
}
