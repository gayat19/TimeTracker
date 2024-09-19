using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdealTimeTracker.WPF.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<TimeSpan>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Activity = table.Column<string>(type: "TEXT", nullable: true),
                    DurationInMins = table.Column<int>(type: "INTEGER", nullable: false),
                    CountPerDay = table.Column<int>(type: "INTEGER", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    EmpId = table.Column<string>(type: "TEXT", nullable: false),
                    PassWord = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    ReportingTo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.EmpId);
                });

            migrationBuilder.CreateTable(
                name: "UserLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmpId = table.Column<string>(type: "TEXT", nullable: true),
                    ActivityId = table.Column<int>(type: "INTEGER", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "TEXT", nullable: true),
                    ActivityAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Reason = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogs_UserActivities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "UserActivities",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "ApplicationConfigurations",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 1, "IDEAL TIME", new TimeSpan(0, 0, 5, 0, 0) });

            migrationBuilder.InsertData(
                table: "ApplicationConfigurations",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 2, "WORKING TIME", new TimeSpan(0, 8, 30, 0, 0) });

            migrationBuilder.InsertData(
                table: "ApplicationConfigurations",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 3, "SYNC TIME ONE", new TimeSpan(0, 4, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "ApplicationConfigurations",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[] { 4, "SYNC TIME TWO", new TimeSpan(0, 13, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "UserActivities",
                columns: new[] { "Id", "Activity", "CountPerDay", "DurationInMins", "IsActive" },
                values: new object[] { 1, "none", null, 0, true });

            migrationBuilder.InsertData(
                table: "UserActivities",
                columns: new[] { "Id", "Activity", "CountPerDay", "DurationInMins", "IsActive" },
                values: new object[] { 2, "login", null, 0, true });

            migrationBuilder.InsertData(
                table: "UserActivities",
                columns: new[] { "Id", "Activity", "CountPerDay", "DurationInMins", "IsActive" },
                values: new object[] { 3, "logout", null, 0, true });

            migrationBuilder.InsertData(
                table: "UserActivities",
                columns: new[] { "Id", "Activity", "CountPerDay", "DurationInMins", "IsActive" },
                values: new object[] { 4, "Others", null, 0, true });

            migrationBuilder.InsertData(
                table: "UserActivities",
                columns: new[] { "Id", "Activity", "CountPerDay", "DurationInMins", "IsActive" },
                values: new object[] { 5, "tea break", 2, 15, true });

            migrationBuilder.InsertData(
                table: "UserActivities",
                columns: new[] { "Id", "Activity", "CountPerDay", "DurationInMins", "IsActive" },
                values: new object[] { 6, "lunch break", 2, 30, true });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_ActivityId",
                table: "UserLogs",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmpId",
                table: "Users",
                column: "EmpId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationConfigurations");

            migrationBuilder.DropTable(
                name: "UserLogs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserActivities");
        }
    }
}
