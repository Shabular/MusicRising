using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicRising.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedLocationAndDetatailsToBandsAndRemovedBankaccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccount",
                table: "Venues");

            migrationBuilder.DropColumn(
                name: "BankAccount",
                table: "Bands");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Bands",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Bands",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Bands",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Bands");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Bands");

            migrationBuilder.AddColumn<string>(
                name: "BankAccount",
                table: "Venues",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BankAccount",
                table: "Bands",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
