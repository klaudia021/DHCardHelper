using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DHCardHelper.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCardSubclassFieldsAndTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BackgroundTypeId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CharacterClassId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BackgroundCardType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackgroundCardType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterClass", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BackgroundTypeId",
                table: "Cards",
                column: "BackgroundTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CharacterClassId",
                table: "Cards",
                column: "CharacterClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_BackgroundCardType_BackgroundTypeId",
                table: "Cards",
                column: "BackgroundTypeId",
                principalTable: "BackgroundCardType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CharacterClass_CharacterClassId",
                table: "Cards",
                column: "CharacterClassId",
                principalTable: "CharacterClass",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_BackgroundCardType_BackgroundTypeId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CharacterClass_CharacterClassId",
                table: "Cards");

            migrationBuilder.DropTable(
                name: "BackgroundCardType");

            migrationBuilder.DropTable(
                name: "CharacterClass");

            migrationBuilder.DropIndex(
                name: "IX_Cards_BackgroundTypeId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CharacterClassId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "BackgroundTypeId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CharacterClassId",
                table: "Cards");
        }
    }
}
