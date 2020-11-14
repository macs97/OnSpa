using Microsoft.EntityFrameworkCore.Migrations;

namespace OnSpa.Web.Migrations
{
    public partial class newData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campuses_Departments_DepartmentsId",
                table: "Campuses");

            migrationBuilder.DropIndex(
                name: "IX_Campuses_DepartmentsId",
                table: "Campuses");

            migrationBuilder.DropColumn(
                name: "DepartmentsId",
                table: "Campuses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentsId",
                table: "Campuses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campuses_DepartmentsId",
                table: "Campuses",
                column: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campuses_Departments_DepartmentsId",
                table: "Campuses",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
