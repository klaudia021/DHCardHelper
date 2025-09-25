using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DHCardHelper.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterAndCardSheetToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterSheet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterSheet_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardSheet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CharacterSheetId = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    InLoadout = table.Column<bool>(type: "bit", nullable: false),
                    InLimit = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardSheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardSheet_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardSheet_CharacterSheet_CharacterSheetId",
                        column: x => x.CharacterSheetId,
                        principalTable: "CharacterSheet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardSheet_CardId",
                table: "CardSheet",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardSheet_CharacterSheetId",
                table: "CardSheet",
                column: "CharacterSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterSheet_UserId",
                table: "CharacterSheet",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardSheet");

            migrationBuilder.DropTable(
                name: "CharacterSheet");
        }
    }
}
