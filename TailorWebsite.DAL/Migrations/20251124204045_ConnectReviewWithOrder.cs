using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TailorWebsite.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ConnectReviewWithOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ServiceReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReviews_OrderId",
                table: "ServiceReviews",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReviews_Orders_OrderId",
                table: "ServiceReviews",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReviews_Orders_OrderId",
                table: "ServiceReviews");

            migrationBuilder.DropIndex(
                name: "IX_ServiceReviews_OrderId",
                table: "ServiceReviews");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ServiceReviews");
        }
    }
}
