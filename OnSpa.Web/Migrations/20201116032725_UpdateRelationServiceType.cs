using Microsoft.EntityFrameworkCore.Migrations;

namespace OnSpa.Web.Migrations
{
    public partial class UpdateRelationServiceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campuses_Cities_CityId",
                table: "Campuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Departments_DepartmentId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypes_Campuses_CampusId",
                table: "ServiceTypes");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTypes_CampusId",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "CampusId",
                table: "ServiceTypes");

            migrationBuilder.CreateTable(
                name: "ServiceTypeCampuses",
                columns: table => new
                {
                    ServiceTypeId = table.Column<int>(nullable: false),
                    CampusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypeCampuses", x => new { x.ServiceTypeId, x.CampusId });
                    table.ForeignKey(
                        name: "FK_ServiceTypeCampuses_Campuses_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceTypeCampuses_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypeCampuses_CampusId",
                table: "ServiceTypeCampuses",
                column: "CampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campuses_Cities_CityId",
                table: "Campuses",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Departments_DepartmentId",
                table: "Cities",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campuses_Cities_CityId",
                table: "Campuses");

            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Departments_DepartmentId",
                table: "Cities");

            migrationBuilder.DropTable(
                name: "ServiceTypeCampuses");

            migrationBuilder.AddColumn<int>(
                name: "CampusId",
                table: "ServiceTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_CampusId",
                table: "ServiceTypes",
                column: "CampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campuses_Cities_CityId",
                table: "Campuses",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Departments_DepartmentId",
                table: "Cities",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypes_Campuses_CampusId",
                table: "ServiceTypes",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
