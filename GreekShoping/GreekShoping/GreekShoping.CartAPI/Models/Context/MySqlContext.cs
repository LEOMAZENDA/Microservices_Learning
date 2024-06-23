using Microsoft.EntityFrameworkCore;

namespace GreekShoping.CartAPI.Models.Context;

public class MySqlContext : DbContext
{
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<CartDetail> Details { get; set; }
    public DbSet<CartHeader> CartHeaders { get; set; }
}
