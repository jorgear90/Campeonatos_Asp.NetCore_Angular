using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampeonatosApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoUsuarioIDEquipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioID",
                table: "Equipos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_UsuarioID",
                table: "Equipos",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Usuarios_UsuarioID",
                table: "Equipos",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Usuarios_UsuarioID",
                table: "Equipos");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_UsuarioID",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Equipos");
        }
    }
}
