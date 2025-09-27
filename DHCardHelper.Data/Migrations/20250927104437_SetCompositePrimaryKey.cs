using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DHCardHelper.Data.Migrations
{
    /// <inheritdoc />
    public partial class SetCompositePrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassToDomainRel",
                table: "ClassToDomainRel");

            migrationBuilder.DropIndex(
                name: "IX_ClassToDomainRel_ClassId",
                table: "ClassToDomainRel");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClassToDomainRel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassToDomainRel",
                table: "ClassToDomainRel",
                columns: new[] { "ClassId", "DomainId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassToDomainRel",
                table: "ClassToDomainRel");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ClassToDomainRel",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassToDomainRel",
                table: "ClassToDomainRel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ClassToDomainRel_ClassId",
                table: "ClassToDomainRel",
                column: "ClassId");
        }
    }
}
