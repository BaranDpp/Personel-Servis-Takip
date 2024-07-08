using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelServisTakip.Migrations
{
    /// <inheritdoc />
    public partial class deneme1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceVehicles_Personels_PersonelId",
                table: "ServiceVehicles");

            migrationBuilder.DropIndex(
                name: "IX_ServiceVehicles_PersonelId",
                table: "ServiceVehicles");

            migrationBuilder.DropColumn(
                name: "PersonelId",
                table: "ServiceVehicles");

            migrationBuilder.AddColumn<int>(
                name: "ServiceVehicleId",
                table: "Personels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Personels_ServiceVehicleId",
                table: "Personels",
                column: "ServiceVehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personels_ServiceVehicles_ServiceVehicleId",
                table: "Personels",
                column: "ServiceVehicleId",
                principalTable: "ServiceVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personels_ServiceVehicles_ServiceVehicleId",
                table: "Personels");

            migrationBuilder.DropIndex(
                name: "IX_Personels_ServiceVehicleId",
                table: "Personels");

            migrationBuilder.DropColumn(
                name: "ServiceVehicleId",
                table: "Personels");

            migrationBuilder.AddColumn<int>(
                name: "PersonelId",
                table: "ServiceVehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceVehicles_PersonelId",
                table: "ServiceVehicles",
                column: "PersonelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceVehicles_Personels_PersonelId",
                table: "ServiceVehicles",
                column: "PersonelId",
                principalTable: "Personels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
