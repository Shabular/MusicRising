using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicRising.Data.Migrations
{
    /// <inheritdoc />
    public partial class addBidByVenueToMakeHaggeling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BidByVenue",
                table: "Shows",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BidByVenue",
                table: "Shows");
        }
    }
}
