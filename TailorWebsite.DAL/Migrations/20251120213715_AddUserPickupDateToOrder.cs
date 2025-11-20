using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TailorWebsite.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPickupDateToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UserPickupDate",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPickupDate",
                table: "Orders");
        }
    }
}
