using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AidKit.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPathImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 3, 18, 26, 41, 775, DateTimeKind.Unspecified).AddTicks(1994), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$vJ7U1eK1wKFLLX1myEKSk.iH46q0FEQzHLIJvvJJ/8RKL9wyEeIry" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 3, 18, 26, 42, 0, DateTimeKind.Unspecified).AddTicks(4567), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$G/u6ubFXpszLFYtnNR.ZNOiQGiVZ90O.4e2bFHDWQagUIv6FrVixK" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 3, 18, 20, 5, 704, DateTimeKind.Unspecified).AddTicks(1303), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$RQsSYhjE3oVpyd0SQzZd9e0KOUhcEfhjoXh.tYyqD6xcN076cUnoa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTimeOffset(new DateTime(2023, 7, 3, 18, 20, 5, 925, DateTimeKind.Unspecified).AddTicks(7394), new TimeSpan(0, 0, 0, 0, 0)), "$2a$11$v2rm2fO0gQl/1LuDe/hZX.6gMo5E6EIKfQRKqB0J9Drj6eY4OMeJy" });
        }
    }
}
