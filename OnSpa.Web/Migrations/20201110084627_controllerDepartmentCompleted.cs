using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnSpa.Web.Migrations
{
    public partial class controllerDepartmentCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceType_Services_ServicesId",
                table: "ServiceType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceType",
                table: "ServiceType");

            migrationBuilder.RenameTable(
                name: "ServiceType",
                newName: "ServiceTypes");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceType_ServicesId",
                table: "ServiceTypes",
                newName: "IX_ServiceTypes_ServicesId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Appointments",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceTypes",
                table: "ServiceTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Document = table.Column<string>(maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    ImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campuses_Name",
                table: "Campuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_UserId",
                table: "Appointments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_User_UserId",
                table: "Appointments",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypes_Services_ServicesId",
                table: "ServiceTypes",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_User_UserId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypes_Services_ServicesId",
                table: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Departments_Name",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Cities_Name",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Campuses_Name",
                table: "Campuses");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_UserId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceTypes",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Appointments");

            migrationBuilder.RenameTable(
                name: "ServiceTypes",
                newName: "ServiceType");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceTypes_ServicesId",
                table: "ServiceType",
                newName: "IX_ServiceType_ServicesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceType",
                table: "ServiceType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceType_Services_ServicesId",
                table: "ServiceType",
                column: "ServicesId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
