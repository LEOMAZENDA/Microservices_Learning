﻿using GreekShoping.OrderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GreekShoping.OrderAPI.Context;

public class MySqlContext : DbContext
{
    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<CartDetail> CartDetails { get; set; }
    public DbSet<CartHeader> CartHeaders { get; set; }
}
