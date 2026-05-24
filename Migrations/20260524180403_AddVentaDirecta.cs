using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Billar306.API.Migrations
{
    /// <inheritdoc />
    public partial class AddVentaDirecta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VentasDirectas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TurnoId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemConfiteriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasDirectas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VentasDirectas_ItemsConfiteria_ItemConfiteriaId",
                        column: x => x.ItemConfiteriaId,
                        principalTable: "ItemsConfiteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VentasDirectas_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VentasDirectas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$om/WJQ10Sj1oSoi3M9WjTegZTchmpmTv7i4zS9ExxJj8z8CVjZ1z2");

            migrationBuilder.CreateIndex(
                name: "IX_VentasDirectas_ItemConfiteriaId",
                table: "VentasDirectas",
                column: "ItemConfiteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_VentasDirectas_TurnoId",
                table: "VentasDirectas",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_VentasDirectas_UsuarioId",
                table: "VentasDirectas",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VentasDirectas");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$jwunJ0gX6I5Bl/70VTtH/OLNyf3FA2X.//rmSAh3mZ34SO5y6EBmy");
        }
    }
}
