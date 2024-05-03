using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicRising.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationToFixDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoItems_Bands_BandId",
                table: "PromoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoItems_Venues_VenueId",
                table: "PromoItems");

            migrationBuilder.DropIndex(
                name: "IX_PromoItems_BandId",
                table: "PromoItems");

            migrationBuilder.DropIndex(
                name: "IX_PromoItems_VenueId",
                table: "PromoItems");

            migrationBuilder.AlterColumn<string>(
                name: "VenueId",
                table: "PromoItems",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "BandId",
                table: "PromoItems",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoItems_Bands_Id",
                table: "PromoItems",
                column: "Id",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoItems_Venues_Id",
                table: "PromoItems",
                column: "Id",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoItems_Bands_Id",
                table: "PromoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoItems_Venues_Id",
                table: "PromoItems");

            migrationBuilder.AlterColumn<string>(
                name: "VenueId",
                table: "PromoItems",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "BandId",
                table: "PromoItems",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PromoItems_BandId",
                table: "PromoItems",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoItems_VenueId",
                table: "PromoItems",
                column: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoItems_Bands_BandId",
                table: "PromoItems",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoItems_Venues_VenueId",
                table: "PromoItems",
                column: "VenueId",
                principalTable: "Venues",
                principalColumn: "Id");
        }
    }
}
