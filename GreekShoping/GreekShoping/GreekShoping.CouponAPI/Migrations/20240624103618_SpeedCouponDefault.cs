using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreekShoping.CouponAPI.Migrations
{
    public partial class SpeedCouponDefault : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TbCoupon",
                columns: new[] { "id", "coupon-code", "discount_amount" },
                values: new object[] { 1L, "LVSAGRADO_2024-10", 10m });

            migrationBuilder.InsertData(
                table: "TbCoupon",
                columns: new[] { "id", "coupon-code", "discount_amount" },
                values: new object[] { 2L, "LVSAGRADO_2024-18", 18m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TbCoupon",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "TbCoupon",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}
