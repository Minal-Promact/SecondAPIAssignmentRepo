using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecondAPIAssignmentRepo.Migrations
{
    /// <inheritdoc />
    public partial class MigrationForEmailInEmp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "employee",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "employee");
        }
    }
}
