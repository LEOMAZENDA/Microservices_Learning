using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreekShoping.ProductApi.Migrations
{
    public partial class SpeedProductsDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.DeleteData(
                table: "TbProduct",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "TbProduct",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "TbProduct",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "TbProduct",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "TbProduct",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "TbProduct",
                keyColumn: "id",
                keyValue: 9L);
        }
    }
}
