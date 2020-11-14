using Microsoft.EntityFrameworkCore.Migrations;

namespace OnSpa.Web.Migrations
{
    public partial class newData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Campuses_campusId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "campusId",
                table: "AspNetUsers",
                newName: "CampusId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_campusId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_CampusId");

            migrationBuilder.AddColumn<int>(
                name: "CampusId",
                table: "Appointments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CampusId",
                table: "Appointments",
                column: "CampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Campuses_CampusId",
                table: "Appointments",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Campuses_CampusId",
                table: "AspNetUsers",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Campuses_CampusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Campuses_CampusId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CampusId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CampusId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "CampusId",
                table: "AspNetUsers",
                newName: "campusId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_CampusId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_campusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Campuses_campusId",
                table: "AspNetUsers",
                column: "campusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
