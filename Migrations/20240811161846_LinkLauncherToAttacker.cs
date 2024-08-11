using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronDome.Migrations
{
    /// <inheritdoc />
    public partial class LinkLauncherToAttacker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AttackerId",
                table: "Launcher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Launcher_AttackerId",
                table: "Launcher",
                column: "AttackerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Launcher_Attacker_AttackerId",
                table: "Launcher",
                column: "AttackerId",
                principalTable: "Attacker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Launcher_Attacker_AttackerId",
                table: "Launcher");

            migrationBuilder.DropIndex(
                name: "IX_Launcher_AttackerId",
                table: "Launcher");

            migrationBuilder.DropColumn(
                name: "AttackerId",
                table: "Launcher");
        }
    }
}
