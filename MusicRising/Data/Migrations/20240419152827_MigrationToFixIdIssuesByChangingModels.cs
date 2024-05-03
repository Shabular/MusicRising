using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicRising.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationToFixIdIssuesByChangingModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoItems_Bands_Id",
                table: "PromoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoItems_Bands_PromoItemId",
                table: "PromoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoItems_Venues_Id",
                table: "PromoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Bands_ShowId",
                table: "Shows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shows",
                table: "Shows");

            migrationBuilder.DropIndex(
                name: "IX_Shows_ShowId",
                table: "Shows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromoItems",
                table: "PromoItems");

            migrationBuilder.DropIndex(
                name: "IX_PromoItems_PromoItemId",
                table: "PromoItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Shows");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Venues",
                newName: "VenueId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Ratings",
                newName: "RatingId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bands",
                newName: "BandId");

            migrationBuilder.UpdateData(
                table: "Shows",
                keyColumn: "ShowId",
                keyValue: null,
                column: "ShowId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ShowId",
                table: "Shows",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "PromoItems",
                keyColumn: "PromoItemId",
                keyValue: null,
                column: "PromoItemId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "PromoItemId",
                table: "PromoItems",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PromoItems",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shows",
                table: "Shows",
                column: "ShowId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromoItems",
                table: "PromoItems",
                column: "PromoItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoItems_Id",
                table: "PromoItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoItems_Bands_Id",
                table: "PromoItems",
                column: "Id",
                principalTable: "Bands",
                principalColumn: "BandId");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoItems_Bands_PromoItemId",
                table: "PromoItems",
                column: "PromoItemId",
                principalTable: "Bands",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoItems_Venues_Id",
                table: "PromoItems",
                column: "Id",
                principalTable: "Venues",
                principalColumn: "VenueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Bands_ShowId",
                table: "Shows",
                column: "ShowId",
                principalTable: "Bands",
                principalColumn: "BandId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromoItems_Bands_Id",
                table: "PromoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoItems_Bands_PromoItemId",
                table: "PromoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PromoItems_Venues_Id",
                table: "PromoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Shows_Bands_ShowId",
                table: "Shows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shows",
                table: "Shows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromoItems",
                table: "PromoItems");

            migrationBuilder.DropIndex(
                name: "IX_PromoItems_Id",
                table: "PromoItems");

            migrationBuilder.RenameColumn(
                name: "VenueId",
                table: "Venues",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RatingId",
                table: "Ratings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BandId",
                table: "Bands",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "ShowId",
                table: "Shows",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Shows",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "PromoItems",
                keyColumn: "Id",
                keyValue: null,
                column: "Id",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PromoItems",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PromoItemId",
                table: "PromoItems",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shows",
                table: "Shows",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromoItems",
                table: "PromoItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_ShowId",
                table: "Shows",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoItems_PromoItemId",
                table: "PromoItems",
                column: "PromoItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoItems_Bands_Id",
                table: "PromoItems",
                column: "Id",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PromoItems_Bands_PromoItemId",
                table: "PromoItems",
                column: "PromoItemId",
                principalTable: "Bands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PromoItems_Venues_Id",
                table: "PromoItems",
                column: "Id",
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shows_Bands_ShowId",
                table: "Shows",
                column: "ShowId",
                principalTable: "Bands",
                principalColumn: "Id");
        }
    }
}
