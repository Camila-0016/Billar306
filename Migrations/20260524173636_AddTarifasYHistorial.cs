using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Billar306.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTarifasYHistorial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ConfiguracionSistema",
                columns: new[] { "Id", "Clave", "Descripcion", "Valor" },
                values: new object[,]
                {
                    { 9, "TarifaHoraMesa", "Precio por hora de mesa", "0" },
                    { 10, "RecargoPorcentajeNocturno", "Porcentaje de recargo nocturno (después de las 6am)", "50" },
                    { 11, "HoraInicioRecargo", "Hora desde la que aplica el recargo (formato 24h)", "6" },
                    { 12, "HoraFinRecargo", "Hora hasta la que aplica el recargo (formato 24h)", "14" }
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$jwunJ0gX6I5Bl/70VTtH/OLNyf3FA2X.//rmSAh3mZ34SO5y6EBmy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfiguracionSistema",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ConfiguracionSistema",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ConfiguracionSistema",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ConfiguracionSistema",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$NWBTmNk2n4iuWLxlOZW5r.4Kh7IqFJSaKfxtYCmdcYVQsj7l479pO");
        }
    }
}
