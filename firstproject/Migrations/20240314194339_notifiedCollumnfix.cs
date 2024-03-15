using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace firstproject.Migrations
{
    /// <inheritdoc />
    public partial class notifiedCollumnfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNotified",
                table: "ToDos",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNotified",
                table: "ToDos");
        }
    }
}
