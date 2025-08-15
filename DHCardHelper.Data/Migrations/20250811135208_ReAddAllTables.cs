using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DHCardHelper.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReAddAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BackgroundCardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackgroundCardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DomainCardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainCardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Feature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DomainId = table.Column<int>(type: "int", nullable: true),
                    DomainCardTypeId = table.Column<int>(type: "int", nullable: true),
                    CharacterClassId = table.Column<int>(type: "int", nullable: true),
                    BackgroundTypeId = table.Column<int>(type: "int", nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: true),
                    Desciption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    RecallCost = table.Column<int>(type: "int", nullable: true),
                    MasteryType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_BackgroundCardTypes_BackgroundTypeId",
                        column: x => x.BackgroundTypeId,
                        principalTable: "BackgroundCardTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_CharacterClasses_CharacterClassId",
                        column: x => x.CharacterClassId,
                        principalTable: "CharacterClasses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_DomainCardTypes_DomainCardTypeId",
                        column: x => x.DomainCardTypeId,
                        principalTable: "DomainCardTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_BackgroundTypeId",
                table: "Cards",
                column: "BackgroundTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CharacterClassId",
                table: "Cards",
                column: "CharacterClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_DomainCardTypeId",
                table: "Cards",
                column: "DomainCardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_DomainId",
                table: "Cards",
                column: "DomainId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "BackgroundCardTypes");

            migrationBuilder.DropTable(
                name: "CharacterClasses");

            migrationBuilder.DropTable(
                name: "DomainCardTypes");

            migrationBuilder.DropTable(
                name: "Domains");
        }
    }
}
