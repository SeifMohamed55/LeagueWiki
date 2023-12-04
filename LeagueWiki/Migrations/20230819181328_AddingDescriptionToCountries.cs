using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueWiki.Migrations
{
    /// <inheritdoc />
    public partial class AddingDescriptionToCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Countries");
        }
    }
}
