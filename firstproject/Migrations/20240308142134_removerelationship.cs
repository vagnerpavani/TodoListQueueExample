using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace firstproject.Migrations
{
    /// <inheritdoc />
    public partial class removerelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Users_UserId1",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_UserId1",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ToDos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ToDos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "ToDos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_UserId1",
                table: "ToDos",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Users_UserId1",
                table: "ToDos",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
