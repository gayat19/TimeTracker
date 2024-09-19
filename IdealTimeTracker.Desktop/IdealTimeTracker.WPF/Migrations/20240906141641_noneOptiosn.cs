using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdealTimeTracker.WPF.Migrations
{
    public partial class noneOptiosn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserActivities",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserActivities",
                columns: new[] { "Id", "Activity", "CountPerDay", "DurationInMins", "IsActive" },
                values: new object[] { 1, "none", null, 0, true });
        }
    }
}
