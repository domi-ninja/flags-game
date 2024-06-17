using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flags_game.Migrations
{
    /// <inheritdoc />
    public partial class coloranalysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "flagColor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    rgb = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flagColor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "colorTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlagColorId = table.Column<int>(type: "INTEGER", nullable: false),
                    FlagId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_colorTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_colorTags_flagColor_FlagColorId",
                        column: x => x.FlagColorId,
                        principalTable: "flagColor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_colorTags_flags_FlagId",
                        column: x => x.FlagId,
                        principalTable: "flags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_colorTags_FlagColorId",
                table: "colorTags",
                column: "FlagColorId");

            migrationBuilder.CreateIndex(
                name: "IX_colorTags_FlagId",
                table: "colorTags",
                column: "FlagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "colorTags");

            migrationBuilder.DropTable(
                name: "flagColor");
        }
    }
}
