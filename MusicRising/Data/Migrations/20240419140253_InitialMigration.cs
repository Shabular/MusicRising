using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicRising.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    IdentityUserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    VenueName = table.Column<string>(type: "varchar(255)", nullable: true),
                    Location = table.Column<int>(type: "int", nullable: false),
                    _bankAccount = table.Column<string>(type: "varchar(255)", nullable: true),
                    Genre = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venues_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Venues_IdentityUserId",
                table: "Venues",
                column: "IdentityUserId");
            
            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    IdentityUserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    BandName = table.Column<string>(type: "varchar(255)", nullable: true),
                    BandPicture = table.Column<string>(type: "varchar(255)", nullable: true),
                    Location = table.Column<int>(type: "int", nullable: true), // Assuming LocationEnum is an integer type
                    _bankAccount = table.Column<string>(type: "varchar(255)", nullable: true),
                    Genre = table.Column<int>(type: "int", nullable: true), // Assuming GenreEnum is an integer type
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bands_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade); // Assuming the relationship is required
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bands_IdentityUserId",
                table: "Bands",
                column: "IdentityUserId");
            
            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    VenueId = table.Column<string>(type: "varchar(255)", nullable: false),
                    BandId = table.Column<string>(type: "varchar(255)", nullable: true),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    PromoLink = table.Column<string>(type: "longtext", nullable: true),
                    ShowFee = table.Column<double>(type: "double", nullable: true),
                    BandFee = table.Column<double>(type: "double", nullable: false),
                    Payed = table.Column<byte>(type: "tinyint(1)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shows_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shows_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shows_BandId",
                table: "Shows",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Shows_VenueId",
                table: "Shows",
                column: "VenueId");
            
            migrationBuilder.CreateTable(
                name: "PromoItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    IdentityUserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    BandId = table.Column<string>(type: "varchar(255)", nullable: true),
                    VenueId = table.Column<string>(type: "varchar(255)", nullable: true),
                    Link = table.Column<string>(type: "text", nullable: true), // Changed from "varchar(max)" to "text"
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromoItems_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade); // Assuming the relationship is required
                    table.ForeignKey(
                        name: "FK_PromoItems_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Adjust the delete behavior if necessary
                    table.ForeignKey(
                        name: "FK_PromoItems_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict); // Adjust the delete behavior if necessary
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromoItems_IdentityUserId",
                table: "PromoItems",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoItems_BandId",
                table: "PromoItems",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoItems_VenueId",
                table: "PromoItems",
                column: "VenueId");
            
            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    IdentityUserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    BandId = table.Column<string>(type: "varchar(255)", nullable: true),
                    VenueId = table.Column<string>(type: "varchar(255)", nullable: true),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: true), // Change varchar(max) to longtext
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                },
        constraints: table =>
        {
            table.PrimaryKey("PK_Ratings", x => x.Id);
            table.ForeignKey(
                name: "FK_Ratings_AspNetUsers_IdentityUserId",
                column: x => x.IdentityUserId,
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade); // Assuming the relationship is required
            table.ForeignKey(
                name: "FK_Ratings_Bands_BandId",
                column: x => x.BandId,
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict); // Adjust the delete behavior if necessary
            table.ForeignKey(
                name: "FK_Ratings_Venues_VenueId",
                column: x => x.VenueId,
                principalTable: "Venues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict); // Adjust the delete behavior if necessary
        });

    migrationBuilder.CreateIndex(
        name: "IX_Ratings_IdentityUserId",
        table: "Ratings",
        column: "IdentityUserId");

    migrationBuilder.CreateIndex(
        name: "IX_Ratings_BandId",
        table: "Ratings",
        column: "BandId");

    migrationBuilder.CreateIndex(
        name: "IX_Ratings_VenueId",
        table: "Ratings",
        column: "VenueId");
            
            
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}