using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flags_game.Migrations
{
    /// <inheritdoc />
    public partial class countryshortname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "shortName",
                table: "flags",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shortName",
                table: "flags");
        }
    }
}
