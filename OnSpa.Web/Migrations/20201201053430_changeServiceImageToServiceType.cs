using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnSpa.Web.Migrations
{
    public partial class changeServiceImageToServiceType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceImages_Services_ServiceId",
                table: "ServiceImages");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "ServiceImages",
                newName: "ServiceTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceImages_ServiceId",
                table: "ServiceImages",
                newName: "IX_ServiceImages_ServiceTypeId");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Services",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Appointments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceImages_ServiceTypes_ServiceTypeId",
                table: "ServiceImages",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceImages_ServiceTypes_ServiceTypeId",
                table: "ServiceImages");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Services");

            migrationBuilder.RenameColumn(
                name: "ServiceTypeId",
                table: "ServiceImages",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceImages_ServiceTypeId",
                table: "ServiceImages",
                newName: "IX_ServiceImages_ServiceId");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "Appointments",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceImages_Services_ServiceId",
                table: "ServiceImages",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
