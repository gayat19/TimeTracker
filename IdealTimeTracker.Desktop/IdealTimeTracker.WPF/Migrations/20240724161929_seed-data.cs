using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdealTimeTracker.WPF.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationConfigurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: new TimeSpan(0, 0, 1, 0, 0));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "EmpId", "Email", "IsActive", "Name", "PassWord", "ReportingTo", "Role" },
                values: new object[] { "ADMIN", "admin@example.com", true, "Admin User", "ADMIN", null, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "EmpId",
                keyValue: "ADMIN");

            migrationBuilder.UpdateData(
                table: "ApplicationConfigurations",
                keyColumn: "Id",
                keyValue: 1,
                column: "Value",
                value: new TimeSpan(0, 0, 5, 0, 0));
        }
    }
}
