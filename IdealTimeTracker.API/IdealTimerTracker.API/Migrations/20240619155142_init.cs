using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdealTImeTracker.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Activity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DurationInMins = table.Column<int>(type: "int", nullable: false),
                    CountPerDay = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordKey = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportingTo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Users_ReportingTo",
                        column: x => x.ReportingTo,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    ActivityAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogs_UserActivities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "UserActivities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserLogs_Users_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserActivities",
                columns: new[] { "Id", "Activity", "CountPerDay", "DurationInMins", "IsActive" },
                values: new object[,]
                {
                    { 1, "none", null, 0, true },
                    { 2, "login", null, 0, true },
                    { 3, "logout", null, 0, true },
                    { 4, "Power Cut", null, 0, true },
                    { 5, "tea break", 2, 15, true },
                    { 6, "lunch break", 2, 30, true }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Name", "PasswordHash", "PasswordKey", "ReportingTo", "Role" },
                values: new object[] { 1, "admin@example.com", true, "Admin User", new byte[] { 44, 215, 222, 75, 218, 181, 206, 164, 172, 188, 227, 43, 60, 168, 91, 110, 211, 175, 237, 193, 14, 101, 194, 235, 3, 3, 49, 168, 44, 175, 101, 45 }, new byte[] { 173, 48, 62, 134, 228, 89, 17, 16, 167, 96, 69, 118, 52, 164, 118, 64, 80, 207, 171, 37, 172, 255, 102, 13, 245, 233, 19, 141, 30, 243, 168, 208, 166, 89, 119, 246, 220, 250, 104, 165, 46, 170, 217, 172, 184, 147, 251, 185, 13, 52, 193, 199, 140, 194, 12, 144, 249, 201, 190, 3, 62, 210, 69, 71 }, null, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_ActivityId",
                table: "UserLogs",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_EmpId",
                table: "UserLogs",
                column: "EmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ReportingTo",
                table: "Users",
                column: "ReportingTo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropTable(
                name: "UserActivities");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
