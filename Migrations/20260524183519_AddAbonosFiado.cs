using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Billar306.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAbonosFiado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MontoAbonado",
                table: "Fiados",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AbonosFiado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FiadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Nota = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbonosFiado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AbonosFiado_Fiados_FiadoId",
                        column: x => x.FiadoId,
                        principalTable: "Fiados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AbonosFiado_Usuarios_UsuarioId",
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
                value: "$2a$11$DzqxW/SL5yYY7PKck3bTLOvdxo94xO/oDcmCwPbggaA5NBYauT6KO");

            migrationBuilder.CreateIndex(
                name: "IX_AbonosFiado_FiadoId",
                table: "AbonosFiado",
                column: "FiadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AbonosFiado_UsuarioId",
                table: "AbonosFiado",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbonosFiado");

            migrationBuilder.DropColumn(
                name: "MontoAbonado",
                table: "Fiados");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$om/WJQ10Sj1oSoi3M9WjTegZTchmpmTv7i4zS9ExxJj8z8CVjZ1z2");
        }
    }
}
