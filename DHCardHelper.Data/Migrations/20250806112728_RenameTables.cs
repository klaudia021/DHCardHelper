using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DHCardHelper.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "_cards",
                newName: "Cards");

            migrationBuilder.RenameTable(
                name: "_domains",
                newName: "Domains ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "_cards"); 

            migrationBuilder.RenameTable(
                name: "Domains",
                newName: "_domains "); 
        }
    }
}
