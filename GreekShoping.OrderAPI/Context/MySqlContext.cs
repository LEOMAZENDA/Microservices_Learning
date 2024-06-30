using GreekShoping.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GreekShoping.OrderAPI.Context;

public class MySqlContext : DbContext
{
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }


    public DbSet<OrderDetail> Details { get; set; }
    public DbSet<OrderHeader> Headers { get; set; }
}
