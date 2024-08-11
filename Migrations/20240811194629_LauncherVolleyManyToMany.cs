using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronDome.Migrations
{
    /// <inheritdoc />
    public partial class LauncherVolleyManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Launcher_Volley_VolleyId",
                table: "Launcher");

            migrationBuilder.DropIndex(
                name: "IX_Launcher_VolleyId",
                table: "Launcher");

            migrationBuilder.DropColumn(
                name: "VolleyId",
                table: "Launcher");

            migrationBuilder.CreateTable(
                name: "LauncherVolley",
                columns: table => new
                {
                    LaunchersId = table.Column<int>(type: "int", nullable: false),
                    VolleysId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LauncherVolley", x => new { x.LaunchersId, x.VolleysId });
                    table.ForeignKey(
                        name: "FK_LauncherVolley_Launcher_LaunchersId",
                        column: x => x.LaunchersId,
                        principalTable: "Launcher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LauncherVolley_Volley_VolleysId",
                        column: x => x.VolleysId,
                        principalTable: "Volley",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LauncherVolley_VolleysId",
                table: "LauncherVolley",
                column: "VolleysId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LauncherVolley");

            migrationBuilder.AddColumn<int>(
                name: "VolleyId",
                table: "Launcher",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Launcher_VolleyId",
                table: "Launcher",
                column: "VolleyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Launcher_Volley_VolleyId",
                table: "Launcher",
                column: "VolleyId",
                principalTable: "Volley",
                principalColumn: "Id");
        }
    }
}
