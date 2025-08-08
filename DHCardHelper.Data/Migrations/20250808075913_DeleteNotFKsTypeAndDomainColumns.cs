using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DHCardHelper.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeleteNotFKsTypeAndDomainColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                table: "Cards",
                name: "Domain"
            );
            migrationBuilder.DropColumn(
                table: "Cards",
                name: "Type"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                table: "Cards",
                name: "Domain",
                type: "nvarchar(max)",
                nullable: true
            );
            migrationBuilder.AddColumn<string>(
                table: "Cards",
                name: "Type",
                type: "nvarchar(max)",
                nullable: true
            );
        }
    }
}
