using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampeonatosApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class ComunaYRegionEquipos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Comunas_ComunaId",
                table: "Equipos");

            migrationBuilder.RenameColumn(
                name: "ComunaId",
                table: "Equipos",
                newName: "ComunaID");

            migrationBuilder.RenameIndex(
                name: "IX_Equipos_ComunaId",
                table: "Equipos",
                newName: "IX_Equipos_ComunaID");

            migrationBuilder.AlterColumn<int>(
                name: "ComunaID",
                table: "Equipos",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Comunas_ComunaID",
                table: "Equipos",
                column: "ComunaID",
                principalTable: "Comunas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_Comunas_ComunaID",
                table: "Equipos");

            migrationBuilder.RenameColumn(
                name: "ComunaID",
                table: "Equipos",
                newName: "ComunaId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipos_ComunaID",
                table: "Equipos",
                newName: "IX_Equipos_ComunaId");

            migrationBuilder.AlterColumn<int>(
                name: "ComunaId",
                table: "Equipos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_Comunas_ComunaId",
                table: "Equipos",
                column: "ComunaId",
                principalTable: "Comunas",
                principalColumn: "Id");
        }
    }
}
