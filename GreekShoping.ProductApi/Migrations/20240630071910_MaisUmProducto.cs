using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreekShoping.ProductApi.Migrations
{
    public partial class MaisUmProducto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TbProduct",
                columns: new[] { "id", "category_name", "description", "image_url", "name", "price" },
                values: new object[] { 10L, "Roupa", "Camisola de homem para Jovens e Adultos ", "~/prodsimages/1_super_mario.jpg", "Camisola - Super Mário 2000", 7500.9m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TbProduct",
                keyColumn: "id",
                keyValue: 10L);
        }
    }
}
