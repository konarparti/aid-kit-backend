using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AidKit.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixPathImageType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PathImage",
                table: "Medicines",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 4, 18, 33, 13, 967, DateTimeKind.Unspecified).AddTicks(974), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$mKbagrewLnsJi8SIVwKjje3Rzi/C8.VRkdlytUnSnFap4RRbNdMOq" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 4, 18, 33, 14, 200, DateTimeKind.Unspecified).AddTicks(7170), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$LUr0EG1hluUsp5IuC9bA9uv8ZHM1Vu5OcPYOhg1S3uMsY0Gg/WEsS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PathImage",
                table: "Medicines",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 3, 19, 34, 59, 988, DateTimeKind.Unspecified).AddTicks(1259), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$sDHRy9g3rwMw0ASrUm1WN.cyyrxr0MU9SxG302M1DVaiKnEkJ2Roy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 3, 19, 35, 0, 197, DateTimeKind.Unspecified).AddTicks(99), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$JqTF0v6RmTQyCieumcpmE.XW7RDsLO3eadMzunnTZCcBj2R.E4EVm" });
        }
    }
}
