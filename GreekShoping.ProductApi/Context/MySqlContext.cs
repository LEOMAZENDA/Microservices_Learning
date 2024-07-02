using GreekShoping.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GreekShoping.ProductApi.Context;

public class MySqlContext : DbContext
{
    public MySqlContext() { }

    public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 4,
            Name = "Mascara Star Wars",
            Description = "Mascara para festas",
            Price = new decimal(25000.00),
            CategoryName = "Brinquedos",
            ImageUrl = "/prodsImages/3_vader.jpg",
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 5,
            Name = "Camisola Preta Gammer",
            Description = "Camisola de homem para Jovens e Adultos",
            Price = new decimal(499.99),
            CategoryName = "Roupa",
            ImageUrl = "/prodsImages/5_100_gamer.jpg",
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 6,
            Name = "Camisola Branca e Preta SpaceX",
            Description = "Camisola de homem para Jovens e Adultos",
            Price = new decimal(7500.90),
            CategoryName = "Roupa",
            ImageUrl = "/prodsImages/6_spacex.jpg",
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 7,
            Name = "Camisola Preta - Occupy Mar",
            Description = "Camisola de homem para Jovens e Adultos",
            Price = new decimal(7500.90),
            CategoryName = "Roupa",
            ImageUrl = "/prodsImages/11_mars.jpg",
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 8,
            Name = "Casaco Capuchinho Preto",
            Description = "Casaco de homem para Jovens e Adultos",
            Price = new decimal(9580.00),
            CategoryName = "Roupa",
            ImageUrl = "/prodsImages/8_moletom_cobra_kay.jpg",
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 9,
            Name = "Camisola Branca - Dragon Ball Z",
            Description = "Camisola de homem para Jovens e Adultos",
            Price = new decimal(7500.90),
            CategoryName = "Roupa",
            ImageUrl = "/prodsImages/13_dragon_ball.jpg",
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 10,
            Name = "Camisola - Super Mário 2000",
            Description = "Camisola de homem para Jovens e Adultos",
            Price = new decimal(7500.90),
            CategoryName = "Roupa",
            ImageUrl = "/prodsImages/1_super_mario.jpg",
        });

        base.OnModelCreating(modelBuilder);
    }
}
