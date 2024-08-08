using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronDome.Migrations
{
    /// <inheritdoc />
    public partial class alterattack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInterceptedOrExploded",
                table: "Attack",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInterceptedOrExploded",
                table: "Attack");
        }
    }
}
