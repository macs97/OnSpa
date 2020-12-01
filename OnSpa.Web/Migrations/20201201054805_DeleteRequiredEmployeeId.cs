using Microsoft.EntityFrameworkCore.Migrations;

namespace OnSpa.Web.Migrations
{
    public partial class DeleteRequiredEmployeeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Appointments",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
