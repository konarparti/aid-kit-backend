using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AidKit.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Medicines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_UserId",
                table: "Medicines",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Users_UserId",
                table: "Medicines",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Users_UserId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_UserId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Medicines");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 3, 18, 51, 15, 774, DateTimeKind.Unspecified).AddTicks(7568), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$h2Ko6hMauG7WKQ6HA6u4iuTKj5TXZ/4VaCwtG5/dnPf9S/Sbxg8BO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 3, 18, 51, 16, 69, DateTimeKind.Unspecified).AddTicks(6317), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$F9zAlvE6PcgKz5PMtbWZEOTAmSMqppJXxVpB4GPT14KA1d0njBw2G" });
        }
    }
}
