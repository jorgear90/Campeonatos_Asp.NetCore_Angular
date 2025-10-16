using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampeonatosApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class ComunasUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComunaID",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_ComunaID",
                table: "Usuarios",
                column: "ComunaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Comunas_ComunaID",
                table: "Usuarios",
                column: "ComunaID",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Comunas_ComunaID",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_ComunaID",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ComunaID",
                table: "Usuarios");
        }
    }
}
