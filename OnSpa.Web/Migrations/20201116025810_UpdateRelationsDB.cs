using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnSpa.Web.Migrations
{
    public partial class UpdateRelationsDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Campuses_CampusId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Campuses_CampusId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CampusId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_CampusId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CampusId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CampusId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "CampusId",
                table: "ServiceTypes",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "ServiceTypes",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ServiceTypes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Appointments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_CampusId",
                table: "ServiceTypes",
                column: "CampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypes_Campuses_CampusId",
                table: "ServiceTypes",
                column: "CampusId",
                principalTable: "Campuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypes_Campuses_CampusId",
                table: "ServiceTypes");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTypes_CampusId",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "CampusId",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Appointments");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Services",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Services",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "CampusId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CampusId",
                table: "Appointments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CampusId",
                table: "AspNetUsers",
                column: "CampusId");

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
    }
}
