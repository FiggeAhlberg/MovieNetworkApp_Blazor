using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb2.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Movies",
                newName: "PosterImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImageUrl",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundImageUrl",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "PosterImageUrl",
                table: "Movies",
                newName: "ImageUrl");
        }
    }
}
