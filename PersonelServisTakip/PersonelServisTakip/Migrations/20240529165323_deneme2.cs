using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelServisTakip.Migrations
{
    /// <inheritdoc />
    public partial class deneme2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Personels");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Personels",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Personels");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Personels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
