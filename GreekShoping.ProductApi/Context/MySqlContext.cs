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
            ImageUrl = "https://github.com/LEOMAZENDA/Microservices_Learning/blob/master/GreekShoping/GreekShoping/ShoppingImages/3_vader.jpg?raw=true",
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 5,
            Name = "Camisola Preta Gammer",
            Description = "Camisola de homem para Jovens e Adultos ",
            Price = new decimal(499.99),
            CategoryName = "Roupa",
            ImageUrl = "https://github.com/LEOMAZENDA/Microservices_Learning/blob/master/GreekShoping/GreekShoping/ShoppingImages/5_100_gamer.jpg?raw=true",
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 6,
            Name = "Camisola Branca e Preta SpaceX",
            Description = "Camisola de homem para Jovens e Adultos ",
            Price = new decimal(7500.90),
            CategoryName = "Roupa",
            ImageUrl = "https://github.com/LEOMAZENDA/Microservices_Learning/blob/master/GreekShoping/GreekShoping/ShoppingImages/6_spacex.jpg?raw=true",
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 7,
            Name = "Camisola Preta - Occupy Mar",
            Description = "Camisola de homem para Jovens e Adultos ",
            Price = new decimal(7500.90),
            CategoryName = "Roupa",
            ImageUrl = "https://github.com/LEOMAZENDA/Microservices_Learning/blob/master/GreekShoping/GreekShoping/ShoppingImages/11_mars.jpg?raw=true",
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 8,
            Name = "Casaco Capuchinho Preto",
            Description = "Casaco de homem para Jovens e Adultos ",
            Price = new decimal(9580.00),
            CategoryName = "Roupa",
            ImageUrl = "https://github.com/LEOMAZENDA/Microservices_Learning/blob/master/GreekShoping/GreekShoping/ShoppingImages/8_moletom_cobra_kay.jpg?raw=true",
        });
        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 9,
            Name = "Camisola Branca - Dragon Ball Z",
            Description = "Camisola de homem para Jovens e Adultos ",
            Price = new decimal(7500.90),
            CategoryName = "Roupa",
            ImageUrl = "https://github.com/LEOMAZENDA/Microservices_Learning/blob/master/GreekShoping/GreekShoping/ShoppingImages/13_dragon_ball.jpg?raw=true",
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = 10,
            Name = "Camisola - Super Mário 2000",
            Description = "Camisola de homem para Jovens e Adultos ",
            Price = new decimal(7500.90),
            CategoryName = "Roupa",
            ImageUrl = "~/prodsimages/1_super_mario.jpg",
        });

        base.OnModelCreating(modelBuilder);
    }
}
