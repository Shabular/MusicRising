using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicRising.Data.Migrations
{
    /// <inheritdoc />
    public partial class itemsToShowData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Payed",
                table: "Shows",
                newName: "IsVenueOwner");

            migrationBuilder.AddColumn<bool>(
                name: "Booked",
                table: "Shows",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBandMember",
                table: "Shows",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Location",
                table: "Bands",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Genre",
                table: "Bands",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Booked",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "IsBandMember",
                table: "Shows");

            migrationBuilder.RenameColumn(
                name: "IsVenueOwner",
                table: "Shows",
                newName: "Payed");

            migrationBuilder.AlterColumn<int>(
                name: "Location",
                table: "Bands",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Genre",
                table: "Bands",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
