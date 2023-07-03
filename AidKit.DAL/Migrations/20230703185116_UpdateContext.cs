using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AidKit.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PainKinds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PainKinds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeMedicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeMedicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PathImage = table.Column<string>(type: "text", nullable: false),
                    Expired = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Available = table.Column<bool>(type: "boolean", nullable: false),
                    TypeMedicineId = table.Column<int>(type: "integer", nullable: false),
                    PainKindId = table.Column<int>(type: "integer", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_PainKinds_PainKindId",
                        column: x => x.PainKindId,
                        principalTable: "PainKinds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medicines_TypeMedicines_TypeMedicineId",
                        column: x => x.TypeMedicineId,
                        principalTable: "TypeMedicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_PainKindId",
                table: "Medicines",
                column: "PainKindId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_TypeMedicineId",
                table: "Medicines",
                column: "TypeMedicineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "PainKinds");

            migrationBuilder.DropTable(
                name: "TypeMedicines");

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
    }
}
