using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreekShoping.OrderAPI.Migrations
{
    public partial class NovasCena : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Details_TbOrderHeader_OrderHeaderId",
                table: "Details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Details",
                table: "Details");

            migrationBuilder.RenameTable(
                name: "Details",
                newName: "TbOrderDetail");

            migrationBuilder.RenameIndex(
                name: "IX_Details_OrderHeaderId",
                table: "TbOrderDetail",
                newName: "IX_TbOrderDetail_OrderHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbOrderDetail",
                table: "TbOrderDetail",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TbOrderDetail_TbOrderHeader_OrderHeaderId",
                table: "TbOrderDetail",
                column: "OrderHeaderId",
                principalTable: "TbOrderHeader",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbOrderDetail_TbOrderHeader_OrderHeaderId",
                table: "TbOrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TbOrderDetail",
                table: "TbOrderDetail");

            migrationBuilder.RenameTable(
                name: "TbOrderDetail",
                newName: "Details");

            migrationBuilder.RenameIndex(
                name: "IX_TbOrderDetail_OrderHeaderId",
                table: "Details",
                newName: "IX_Details_OrderHeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Details",
                table: "Details",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Details_TbOrderHeader_OrderHeaderId",
                table: "Details",
                column: "OrderHeaderId",
                principalTable: "TbOrderHeader",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
