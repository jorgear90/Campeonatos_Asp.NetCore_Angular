using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampeonatosApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class ComunasAndRegiones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Usuarios_UsuarioID",
                table: "Equipos");

            migrationBuilder.AddColumn<int>(
                name: "ComunaId",
                table: "Equipos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Regiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comunas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comunas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comunas_Regiones_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Regiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_ComunaId",
                table: "Equipos",
                column: "ComunaId");

            migrationBuilder.CreateIndex(
                name: "IX_Comunas_RegionID",
                table: "Comunas",
                column: "RegionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Comunas_ComunaId",
                table: "Equipos",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Usuarios_UsuarioID",
                table: "Equipos",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Comunas_ComunaId",
                table: "Equipos");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Usuarios_UsuarioID",
                table: "Equipos");

            migrationBuilder.DropTable(
                name: "Comunas");

            migrationBuilder.DropTable(
                name: "Regiones");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_ComunaId",
                table: "Equipos");

            migrationBuilder.DropColumn(
                name: "ComunaId",
                table: "Equipos");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Usuarios_UsuarioID",
                table: "Equipos",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
