using Microsoft.EntityFrameworkCore.Migrations;

namespace OnSpa.Web.Migrations
{
    public partial class Rol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Appointments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Appointments",
                nullable: true);
        }
    }
}
