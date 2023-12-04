using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueWiki.Migrations
{
    /// <inheritdoc />
    public partial class AddingImagePathToRoleAndCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Countries");
        }
    }
}
