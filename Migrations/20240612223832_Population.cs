using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace flags_game.Migrations
{
    /// <inheritdoc />
    public partial class Population : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "population",
                table: "flags",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_flagTags_FlagId",
                table: "flagTags",
                column: "FlagId");

            migrationBuilder.CreateIndex(
                name: "IX_flagTags_TagId",
                table: "flagTags",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_flagTags_flags_FlagId",
                table: "flagTags",
                column: "FlagId",
                principalTable: "flags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flagTags_tags_TagId",
                table: "flagTags",
                column: "TagId",
                principalTable: "tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flagTags_flags_FlagId",
                table: "flagTags");

            migrationBuilder.DropForeignKey(
                name: "FK_flagTags_tags_TagId",
                table: "flagTags");

            migrationBuilder.DropIndex(
                name: "IX_flagTags_FlagId",
                table: "flagTags");

            migrationBuilder.DropIndex(
                name: "IX_flagTags_TagId",
                table: "flagTags");

            migrationBuilder.DropColumn(
                name: "population",
                table: "flags");
        }
    }
}
