using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DHCardHelper.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameTableCharacterClassAndBackgroundCardType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "BackgroundCardType",
                newName: "BackgroundCardTypes");

            migrationBuilder.RenameTable(
                name: "CharacterClass",
                newName: "CharacterClasses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "BackgroundCardTypes",
                newName: "BackgroundCardType");

            migrationBuilder.RenameTable(
                name: "CharacterClasses",
                newName: "CharacterClass");
        }
    }
}
