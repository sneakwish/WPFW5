using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace wpfwopdracht5.Migrations
{
    public partial class MijnEersteMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Huurders",
                columns: table => new
                {
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Mobile = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huurders", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Verhuurders",
                columns: table => new
                {
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Mobile = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verhuurders", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Autos",
                columns: table => new
                {
                    Brand = table.Column<string>(type: "TEXT", nullable: false),
                    VerhuurderEmail = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autos", x => x.Brand);
                    table.ForeignKey(
                        name: "FK_Autos_Verhuurders_VerhuurderEmail",
                        column: x => x.VerhuurderEmail,
                        principalTable: "Verhuurders",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Period",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BorrowTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    AutoBrand = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Period", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Period_Autos_AutoBrand",
                        column: x => x.AutoBrand,
                        principalTable: "Autos",
                        principalColumn: "Brand",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Huurders",
                columns: new[] { "Email", "Mobile", "Name" },
                values: new object[] { "huurman@gmail.com", "0638952354", "Huurman1" });

            migrationBuilder.InsertData(
                table: "Huurders",
                columns: new[] { "Email", "Mobile", "Name" },
                values: new object[] { "buurman@gmail.com", "063852685", "buurman" });

            migrationBuilder.InsertData(
                table: "Verhuurders",
                columns: new[] { "Email", "Address", "Mobile", "Name" },
                values: new object[] { "kevin@gmail.com", "beverstraat 25", "063752655", "kevin" });

            migrationBuilder.InsertData(
                table: "Verhuurders",
                columns: new[] { "Email", "Address", "Mobile", "Name" },
                values: new object[] { "Wishal@gmail.com", "kalrestraat 32", "063752652", "Wishal" });

            migrationBuilder.InsertData(
                table: "Autos",
                columns: new[] { "Brand", "VerhuurderEmail" },
                values: new object[] { "Tesla", "kevin@gmail.com" });

            migrationBuilder.InsertData(
                table: "Autos",
                columns: new[] { "Brand", "VerhuurderEmail" },
                values: new object[] { "Volkswagen", "Wishal@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_Autos_VerhuurderEmail",
                table: "Autos",
                column: "VerhuurderEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Period_AutoBrand",
                table: "Period",
                column: "AutoBrand");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Huurders");

            migrationBuilder.DropTable(
                name: "Period");

            migrationBuilder.DropTable(
                name: "Autos");

            migrationBuilder.DropTable(
                name: "Verhuurders");
        }
    }
}
