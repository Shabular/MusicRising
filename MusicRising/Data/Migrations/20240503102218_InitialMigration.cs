using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicRising.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Bands table
            migrationBuilder.CreateTable(
                name: "Bands",
                columns: table => new
                {
                    BandId = table.Column<string>(type: "varchar(255)", nullable: false),
                    BandName = table.Column<string>(type: "varchar(255)", nullable: true),
                    BandPicture = table.Column<string>(type: "varchar(255)", nullable: true),
                    BankAccount = table.Column<string>(type: "longtext", nullable: true),
                    Genre = table.Column<int>(nullable: true),  // Corrected type
                    IdentityUserId = table.Column<string>(type: "varchar(255)", nullable: true),
                    Location = table.Column<int>(nullable: true)  // Corrected type
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bands", x => x.BandId);
                });

            // Create Venues table
            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueId = table.Column<string>(type: "varchar(255)", nullable: false),
                    VenueName = table.Column<string>(type: "varchar(255)", nullable: true),
                    BankAccount = table.Column<string>(type: "longtext", nullable: true),
                    Genre = table.Column<int>(nullable: true),  // Corrected type
                    IdentityUserId = table.Column<string>(type: "varchar(255)", nullable: true),
                    Location = table.Column<int>(nullable: true)  // Corrected type
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.VenueId);
                });

            // Create Shows table
            migrationBuilder.CreateTable(
                name: "Shows",
                columns: table => new
                {
                    ShowId = table.Column<string>(type: "varchar(255)", nullable: false),
                    BandId = table.Column<string>(type: "varchar(255)", nullable: true),
                    VenueId = table.Column<string>(type: "varchar(255)", nullable: true),
                    Genre = table.Column<int>(nullable: true),  // Corrected type
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PromoLink = table.Column<string>(type: "longtext", nullable: true),
                    ShowFee = table.Column<double>(nullable: true),
                    BandFee = table.Column<double>(type: "double", nullable: false),
                    Payed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shows", x => x.ShowId);
                    table.ForeignKey(
                        name: "FK_Shows_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "BandId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Shows_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create PromoItems table
            migrationBuilder.CreateTable(
                name: "PromoItems",
                columns: table => new
                {
                    PromoItemId = table.Column<string>(type: "varchar(255)", nullable: false),
                    BandId = table.Column<string>(type: "varchar(255)", nullable: true),
                    VenueId = table.Column<string>(type: "varchar(255)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoItems", x => x.PromoItemId);
                    table.ForeignKey(
                        name: "FK_PromoItems_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "BandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromoItems_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create Ratings table
            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<string>(type: "varchar(255)", nullable: false),
                    BandId = table.Column<string>(type: "varchar(255)", nullable: true),
                    VenueId = table.Column<string>(type: "varchar(255)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Ratings_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "BandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PromoItems");
            migrationBuilder.DropTable(name: "Ratings");
            migrationBuilder.DropTable(name: "Shows");
            migrationBuilder.DropTable(name: "Bands");
            migrationBuilder.DropTable(name: "Venues");
        }
    }
}
