using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DHCardHelper.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeSpaceFromDomainsTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Domains ",
                newName: "Domains");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Domains",
                newName: "Domains ");
        }
    }
}
