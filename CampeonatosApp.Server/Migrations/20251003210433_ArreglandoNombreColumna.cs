using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampeonatosApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class ArreglandoNombreColumna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cotraseña",
                table: "Usuarios",
                newName: "Contraseña");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "Usuarios",
                newName: "Cotraseña");
        }
    }
}
