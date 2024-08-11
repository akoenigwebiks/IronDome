using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IronDome.Migrations
{
    /// <inheritdoc />
    public partial class AmmoAddVolleyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Add the column as nullable
            migrationBuilder.AddColumn<int>(
                name: "VolleyId",
                table: "Ammo",
                type: "int",
                nullable: true);

            // Step 2: Update existing Ammo records to a valid VolleyId
            // This assumes you have some logic to determine the correct VolleyId, or you may need to set them to a default valid VolleyId.
            migrationBuilder.Sql("UPDATE Ammo SET VolleyId = (SELECT TOP 1 Id FROM Volley WHERE Volley.Id = LauncherId) WHERE VolleyId IS NULL");

            // Step 3: Alter the column to be non-nullable
            migrationBuilder.AlterColumn<int>(
                name: "VolleyId",
                table: "Ammo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // Step 4: Add the foreign key constraint
            migrationBuilder.CreateIndex(
                name: "IX_Ammo_VolleyId",
                table: "Ammo",
                column: "VolleyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ammo_Volley_VolleyId",
                table: "Ammo",
                column: "VolleyId",
                principalTable: "Volley",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ammo_Volley_VolleyId",
                table: "Ammo");

            migrationBuilder.DropIndex(
                name: "IX_Ammo_VolleyId",
                table: "Ammo");

            migrationBuilder.DropColumn(
                name: "VolleyId",
                table: "Ammo");
        }
    }
}
