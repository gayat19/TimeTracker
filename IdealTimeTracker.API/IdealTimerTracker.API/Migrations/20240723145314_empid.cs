using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdealTImeTracker.API.Migrations
{
    public partial class empid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_Users_EmpId",
                table: "UserLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ReportingTo",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordKey",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "ReportingTo",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmpId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassWord",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "EmpId",
                table: "UserLogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "EmpId");

            migrationBuilder.UpdateData(
                table: "UserActivities",
                keyColumn: "Id",
                keyValue: 4,
                column: "Activity",
                value: "Others");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "EmpId", "Email", "IsActive", "Name", "PassWord", "ReportingTo", "Role" },
                values: new object[] { "ADMIN", "admin@example.com", true, "Admin User", "ADMIN", null, "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_Users_EmpId",
                table: "UserLogs",
                column: "EmpId",
                principalTable: "Users",
                principalColumn: "EmpId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ReportingTo",
                table: "Users",
                column: "ReportingTo",
                principalTable: "Users",
                principalColumn: "EmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_Users_EmpId",
                table: "UserLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_ReportingTo",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "EmpId",
                keyColumnType: "nvarchar(450)",
                keyValue: "ADMIN");

            migrationBuilder.DropColumn(
                name: "EmpId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PassWord",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "ReportingTo",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordKey",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmpId",
                table: "UserLogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "UserActivities",
                keyColumn: "Id",
                keyValue: 4,
                column: "Activity",
                value: "Power Cut");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Name", "PasswordHash", "PasswordKey", "ReportingTo", "Role" },
                values: new object[] { 1, "admin@example.com", true, "Admin User", new byte[] { 122, 125, 71, 210, 89, 188, 170, 228, 217, 123, 95, 186, 114, 236, 231, 137, 80, 71, 249, 139, 71, 136, 150, 26, 100, 226, 195, 237, 111, 112, 5, 236 }, new byte[] { 57, 231, 29, 54, 250, 29, 108, 7, 28, 1, 143, 8, 215, 88, 107, 20, 70, 144, 121, 33, 135, 217, 136, 105, 130, 64, 60, 254, 90, 226, 201, 113, 21, 182, 19, 84, 191, 84, 76, 155, 79, 86, 85, 209, 92, 145, 132, 250, 2, 74, 45, 116, 249, 183, 160, 199, 28, 216, 223, 190, 1, 2, 33, 134 }, null, "admin" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_Users_EmpId",
                table: "UserLogs",
                column: "EmpId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_ReportingTo",
                table: "Users",
                column: "ReportingTo",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
