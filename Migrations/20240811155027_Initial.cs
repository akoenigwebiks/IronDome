using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronDome.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attacker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attacker", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Volley",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaunchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttackerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volley", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Volley_Attacker_AttackerId",
                        column: x => x.AttackerId,
                        principalTable: "Attacker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Launcher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Range = table.Column<int>(type: "int", nullable: false),
                    Velocity = table.Column<int>(type: "int", nullable: false),
                    VolleyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Launcher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Launcher_Volley_VolleyId",
                        column: x => x.VolleyId,
                        principalTable: "Volley",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ammo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LauncherId = table.Column<int>(type: "int", nullable: false),
                    IsDestroyed = table.Column<bool>(type: "bit", nullable: false),
                    IsLaunched = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ammo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ammo_Launcher_LauncherId",
                        column: x => x.LauncherId,
                        principalTable: "Launcher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ammo_LauncherId",
                table: "Ammo",
                column: "LauncherId");

            migrationBuilder.CreateIndex(
                name: "IX_Launcher_VolleyId",
                table: "Launcher",
                column: "VolleyId");

            migrationBuilder.CreateIndex(
                name: "IX_Volley_AttackerId",
                table: "Volley",
                column: "AttackerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ammo");

            migrationBuilder.DropTable(
                name: "Launcher");

            migrationBuilder.DropTable(
                name: "Volley");

            migrationBuilder.DropTable(
                name: "Attacker");
        }
    }
}
