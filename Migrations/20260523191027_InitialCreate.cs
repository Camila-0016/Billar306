using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Billar306.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientesFrecuentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreCompleto = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesFrecuentes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionSistema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Clave = table.Column<string>(type: "TEXT", nullable: false),
                    Valor = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionSistema", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsConfiteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false),
                    StockActual = table.Column<int>(type: "INTEGER", nullable: false),
                    StockMinimo = table.Column<int>(type: "INTEGER", nullable: false),
                    EsBebidaNoAlcoholica = table.Column<bool>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsConfiteria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Rol = table.Column<string>(type: "TEXT", nullable: false),
                    NombreCompleto = table.Column<string>(type: "TEXT", nullable: false),
                    SueldoBase = table.Column<decimal>(type: "TEXT", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaApertura = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MontoAperturaCaja = table.Column<decimal>(type: "TEXT", nullable: false),
                    MontoCierreFisico = table.Column<decimal>(type: "TEXT", nullable: true),
                    MontoEsperado = table.Column<decimal>(type: "TEXT", nullable: true),
                    DiferenciaCaja = table.Column<decimal>(type: "TEXT", nullable: true),
                    GravedadDiferencia = table.Column<string>(type: "TEXT", nullable: false),
                    NotaCierre = table.Column<string>(type: "TEXT", nullable: true),
                    AperturaCompleta = table.Column<bool>(type: "INTEGER", nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turnos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Anticipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmpleadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    TurnoId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioAutorizanteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ForzadoPorJefe = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anticipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anticipos_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Anticipos_Usuarios_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anticipos_Usuarios_UsuarioAutorizanteId",
                        column: x => x.UsuarioAutorizanteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventosTurno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TurnoId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoEvento = table.Column<string>(type: "TEXT", nullable: false),
                    Gravedad = table.Column<string>(type: "TEXT", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Revisado = table.Column<bool>(type: "INTEGER", nullable: false),
                    NotaRevision = table.Column<string>(type: "TEXT", nullable: true),
                    UsuarioRevisionId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventosTurno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventosTurno_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventosTurno_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SesionesMesa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MesaId = table.Column<int>(type: "INTEGER", nullable: false),
                    TurnoId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClienteFrecuenteId = table.Column<int>(type: "INTEGER", nullable: true),
                    Inicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Fin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TotalCobrado = table.Column<decimal>(type: "TEXT", nullable: true),
                    Estado = table.Column<string>(type: "TEXT", nullable: false),
                    MontoRecibido = table.Column<decimal>(type: "TEXT", nullable: true),
                    Vuelto = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionesMesa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SesionesMesa_ClientesFrecuentes_ClienteFrecuenteId",
                        column: x => x.ClienteFrecuenteId,
                        principalTable: "ClientesFrecuentes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SesionesMesa_Mesas_MesaId",
                        column: x => x.MesaId,
                        principalTable: "Mesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionesMesa_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionesMesa_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ConsumicionesMesa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SesionMesaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemConfiteriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumicionesMesa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumicionesMesa_ItemsConfiteria_ItemConfiteriaId",
                        column: x => x.ItemConfiteriaId,
                        principalTable: "ItemsConfiteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsumicionesMesa_SesionesMesa_SesionMesaId",
                        column: x => x.SesionMesaId,
                        principalTable: "SesionesMesa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fiados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SesionMesaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClienteFrecuenteId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioRegistroId = table.Column<int>(type: "INTEGER", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Prenda = table.Column<string>(type: "TEXT", nullable: false),
                    DescripcionPrenda = table.Column<string>(type: "TEXT", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Estado = table.Column<string>(type: "TEXT", nullable: false),
                    UsuarioCierreId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fiados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fiados_ClientesFrecuentes_ClienteFrecuenteId",
                        column: x => x.ClienteFrecuenteId,
                        principalTable: "ClientesFrecuentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fiados_SesionesMesa_SesionMesaId",
                        column: x => x.SesionMesaId,
                        principalTable: "SesionesMesa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Fiados_Usuarios_UsuarioRegistroId",
                        column: x => x.UsuarioRegistroId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ConfiguracionSistema",
                columns: new[] { "Id", "Clave", "Descripcion", "Valor" },
                values: new object[,]
                {
                    { 1, "LimiteAnticipoPorc", "Porcentaje máximo de anticipo sobre sueldo", "40" },
                    { 2, "MontoMaxGravedadBaja", "Diferencia de caja máxima para gravedad baja", "2000" },
                    { 3, "PeriodoRedondeo", "Días del período de redondeo a favor del cliente", "30" },
                    { 4, "PausaMinimaJuegoMin", "Minutos mínimos entre sesiones de juego del mismo empleado", "30" },
                    { 5, "ModalidadJuego", "Modalidad de juego: tiempo | consumicion | ambas", "consumicion" },
                    { 6, "DuracionTokenHoras", "Horas de validez del token JWT", "8" }
                });

            migrationBuilder.InsertData(
                table: "Mesas",
                columns: new[] { "Id", "Estado", "Numero" },
                values: new object[,]
                {
                    { 1, "Libre", 1 },
                    { 2, "Libre", 2 },
                    { 3, "Libre", 3 },
                    { 4, "Libre", 4 },
                    { 5, "Libre", 5 },
                    { 6, "Libre", 6 },
                    { 7, "Libre", 7 },
                    { 8, "Libre", 8 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Activo", "FechaCreacion", "NombreCompleto", "NombreUsuario", "PasswordHash", "Rol", "SueldoBase" },
                values: new object[] { 1, true, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administrador", "jefe", "$2a$11$vYybZ4n8pttLAkS0y/vQ7evXWgWfdDU6Ckbgvz9Imccmx3pCo.zBq", "jefe", 0m });

            migrationBuilder.CreateIndex(
                name: "IX_Anticipos_EmpleadoId",
                table: "Anticipos",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anticipos_TurnoId",
                table: "Anticipos",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anticipos_UsuarioAutorizanteId",
                table: "Anticipos",
                column: "UsuarioAutorizanteId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionSistema_Clave",
                table: "ConfiguracionSistema",
                column: "Clave",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsumicionesMesa_ItemConfiteriaId",
                table: "ConsumicionesMesa",
                column: "ItemConfiteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumicionesMesa_SesionMesaId",
                table: "ConsumicionesMesa",
                column: "SesionMesaId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosTurno_TurnoId",
                table: "EventosTurno",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosTurno_UsuarioId",
                table: "EventosTurno",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Fiados_ClienteFrecuenteId",
                table: "Fiados",
                column: "ClienteFrecuenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Fiados_SesionMesaId",
                table: "Fiados",
                column: "SesionMesaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fiados_UsuarioRegistroId",
                table: "Fiados",
                column: "UsuarioRegistroId");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMesa_ClienteFrecuenteId",
                table: "SesionesMesa",
                column: "ClienteFrecuenteId");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMesa_MesaId",
                table: "SesionesMesa",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMesa_TurnoId",
                table: "SesionesMesa",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMesa_UsuarioId",
                table: "SesionesMesa",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_UsuarioId",
                table: "Turnos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NombreUsuario",
                table: "Usuarios",
                column: "NombreUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anticipos");

            migrationBuilder.DropTable(
                name: "ConfiguracionSistema");

            migrationBuilder.DropTable(
                name: "ConsumicionesMesa");

            migrationBuilder.DropTable(
                name: "EventosTurno");

            migrationBuilder.DropTable(
                name: "Fiados");

            migrationBuilder.DropTable(
                name: "ItemsConfiteria");

            migrationBuilder.DropTable(
                name: "SesionesMesa");

            migrationBuilder.DropTable(
                name: "ClientesFrecuentes");

            migrationBuilder.DropTable(
                name: "Mesas");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
