using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Billar306.API.Migrations
{
    /// <inheritdoc />
    public partial class AddLiquidacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsBebidaNoAlcoholica",
                table: "ItemsConfiteria");

            migrationBuilder.AddColumn<decimal>(
                name: "EfectivoConfiteria",
                table: "Turnos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EfectivoMaquinas",
                table: "Turnos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RegistrosHora",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    TurnoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Entrada = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Salida = table.Column<DateTime>(type: "TEXT", nullable: true),
                    HorasTrabajadas = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosHora", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosHora_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrosHora_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ConfiguracionSistema",
                columns: new[] { "Id", "Clave", "Descripcion", "Valor" },
                values: new object[,]
                {
                    { 7, "TarifaHoraEmpleado", "Tarifa por hora para empleados", "0" },
                    { 8, "TarifaHoraEncargado", "Tarifa por hora para encargados", "0" }
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$NWBTmNk2n4iuWLxlOZW5r.4Kh7IqFJSaKfxtYCmdcYVQsj7l479pO");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosHora_TurnoId",
                table: "RegistrosHora",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosHora_UsuarioId",
                table: "RegistrosHora",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosHora");

            migrationBuilder.DeleteData(
                table: "ConfiguracionSistema",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ConfiguracionSistema",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DropColumn(
                name: "EfectivoConfiteria",
                table: "Turnos");

            migrationBuilder.DropColumn(
                name: "EfectivoMaquinas",
                table: "Turnos");

            migrationBuilder.AddColumn<bool>(
                name: "EsBebidaNoAlcoholica",
                table: "ItemsConfiteria",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$9cA8kP5sIwFk5gfTHdXXeeuW.SglcnG1M88/3wCVin8i841Rtod5.");
        }
    }
}
