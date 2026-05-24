using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Billar306.API.Migrations
{
    /// <inheritdoc />
    public partial class AjusteAperturaTurno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockApertura",
                table: "ItemsConfiteria",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "IngresosStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TurnoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemConfiteriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nota = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresosStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngresosStock_ItemsConfiteria_ItemConfiteriaId",
                        column: x => x.ItemConfiteriaId,
                        principalTable: "ItemsConfiteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngresosStock_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngresosStock_Usuarios_UsuarioId",
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
                value: "$2a$11$9cA8kP5sIwFk5gfTHdXXeeuW.SglcnG1M88/3wCVin8i841Rtod5.");

            migrationBuilder.CreateIndex(
                name: "IX_IngresosStock_ItemConfiteriaId",
                table: "IngresosStock",
                column: "ItemConfiteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_IngresosStock_TurnoId",
                table: "IngresosStock",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_IngresosStock_UsuarioId",
                table: "IngresosStock",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngresosStock");

            migrationBuilder.DropColumn(
                name: "StockApertura",
                table: "ItemsConfiteria");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$vYybZ4n8pttLAkS0y/vQ7evXWgWfdDU6Ckbgvz9Imccmx3pCo.zBq");
        }
    }
}
