using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flags_game.Migrations
{
    /// <inheritdoc />
    public partial class colornames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "flagColor",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "flagColor");
        }
    }
}
