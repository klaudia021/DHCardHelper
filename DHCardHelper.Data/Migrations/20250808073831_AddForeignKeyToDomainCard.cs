using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DHCardHelper.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToDomainCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Cards",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DomainId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_DomainId",
                table: "Cards",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_TypeId",
                table: "Cards",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Domains_DomainId",
                table: "Cards",
                column: "DomainId",
                principalTable: "Domains",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Types_TypeId",
                table: "Cards",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Domains_DomainId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Types_TypeId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_DomainId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_TypeId",
                table: "Cards");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
