using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdealTImeTracker.API.Migrations
{
    public partial class config : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "UserLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationConfigurations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ApplicationConfigurations",
                columns: new[] { "Id", "Name", "Value" },
                values: new object[,]
                {
                    { 1, "IDEAL TIME", new TimeSpan(0, 0, 5, 0, 0) },
                    { 2, "WORKING TIME", new TimeSpan(0, 0, 5, 0, 0) },
                    { 3, "SYNC TIME ONE", new TimeSpan(0, 4, 0, 0, 0) },
                    { 4, "SYNC TIME TWO", new TimeSpan(0, 13, 0, 0, 0) }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordKey" },
                values: new object[] { new byte[] { 122, 125, 71, 210, 89, 188, 170, 228, 217, 123, 95, 186, 114, 236, 231, 137, 80, 71, 249, 139, 71, 136, 150, 26, 100, 226, 195, 237, 111, 112, 5, 236 }, new byte[] { 57, 231, 29, 54, 250, 29, 108, 7, 28, 1, 143, 8, 215, 88, 107, 20, 70, 144, 121, 33, 135, 217, 136, 105, 130, 64, 60, 254, 90, 226, 201, 113, 21, 182, 19, 84, 191, 84, 76, 155, 79, 86, 85, 209, 92, 145, 132, 250, 2, 74, 45, 116, 249, 183, 160, 199, 28, 216, 223, 190, 1, 2, 33, 134 } });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationConfigurations");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "UserLogs");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordKey" },
                values: new object[] { new byte[] { 44, 215, 222, 75, 218, 181, 206, 164, 172, 188, 227, 43, 60, 168, 91, 110, 211, 175, 237, 193, 14, 101, 194, 235, 3, 3, 49, 168, 44, 175, 101, 45 }, new byte[] { 173, 48, 62, 134, 228, 89, 17, 16, 167, 96, 69, 118, 52, 164, 118, 64, 80, 207, 171, 37, 172, 255, 102, 13, 245, 233, 19, 141, 30, 243, 168, 208, 166, 89, 119, 246, 220, 250, 104, 165, 46, 170, 217, 172, 184, 147, 251, 185, 13, 52, 193, 199, 140, 194, 12, 144, 249, 201, 190, 3, 62, 210, 69, 71 } });
        }
    }
}
