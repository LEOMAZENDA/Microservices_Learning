﻿using Microsoft.EntityFrameworkCore;

namespace GreekShoping.ProductApi.Models.Context;

public class MySqlContext : DbContext
{
    public MySqlContext() { }

    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
}