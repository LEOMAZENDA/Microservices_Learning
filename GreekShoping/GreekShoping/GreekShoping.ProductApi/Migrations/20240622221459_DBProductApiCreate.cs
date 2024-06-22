using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreekShoping.ProductApi.Migrations
{
    public partial class DBProductApiCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TbProduct",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image_url = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbProduct", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "TbProduct",
                columns: new[] { "id", "category_name", "description", "image_url", "name", "price" },
                values: new object[,]
                {
                    { 4L, "Brinquedos", "Mascara para festas", "https://github.com/LEOMAZENDA/Microservices_Learning/blob/master/GreekShoping/GreekShoping/ShoppingImages/3_vader.jpg?raw=true", "Mascara Star Wars", 25000m },
                    { 5L, "Roupa", "Camisola de homem para Jovens e Adultos ", "https://github.com/LEOMAZENDA/Microservices_Learning/blob/master/GreekShoping/GreekShoping/ShoppingImages/5_100_gamer.jpg?raw=true", "Camisola Preta Gammer", 499.99m },
                    { 6L, "Roupa", "Camisola de homem para Jovens e Adultos ", "https://github.com/LEOMAZENDA/Microservices_Learning/blob/master/GreekShoping/GreekShoping/ShoppingImages/6_spacex.jpg?raw=true", "Camisola Branca e Preta SpaceX", 7500.9m },
                    { 7L, "Roupa", "Camisola de homem para Jovens e Adultos ", "https://github.com/LEOMAZENDA/Microservices_Learning/blob/master/GreekShoping/GreekShoping/ShoppingImages/11_mars.jpg?raw=true", "Camisola Preta - Occupy Mar", 7500.9m },
                    { 8L, "Roupa", "Casaco de homem para Jovens e Adultos ", "https://github.com/LEOMAZENDA/Microservices_Learning/blob/master/GreekShoping/GreekShoping/ShoppingImages/8_moletom_cobra_kay.jpg?raw=true", "Casaco Capuchinho Preto", 9580m },
                    { 9L, "Roupa", "Camisola de homem para Jovens e Adultos ", "https://github.com/LEOMAZENDA/Microservices_Learning/blob/master/GreekShoping/GreekShoping/ShoppingImages/13_dragon_ball.jpg?raw=true", "Camisola Branca - Dragon Ball Z", 7500.9m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbProduct");
        }
    }
}
