using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreekShoping.CartAPI.Migrations
{
    public partial class UpdateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "count",
                table: "TbCartDetail",
                newName: "Count");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "TbCartDetail",
                newName: "count");
        }
    }
}
