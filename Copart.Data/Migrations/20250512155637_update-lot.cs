using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Copart.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatelot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Lots",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Lots");
        }
    }
}
