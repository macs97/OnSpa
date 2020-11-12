using Microsoft.EntityFrameworkCore.Migrations;

namespace OnSpa.Web.Migrations
{
    public partial class updateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "campusId",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentsId",
                table: "Campuses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_campusId",
                table: "User",
                column: "campusId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_User_Campuses_campusId",
                table: "User",
                column: "campusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campuses_Departments_DepartmentsId",
                table: "Campuses");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Campuses_campusId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_campusId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Campuses_DepartmentsId",
                table: "Campuses");

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "User");

            migrationBuilder.DropColumn(
                name: "campusId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DepartmentsId",
                table: "Campuses");
        }
    }
}
