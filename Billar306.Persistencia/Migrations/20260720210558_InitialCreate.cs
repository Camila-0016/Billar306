using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Billar306.Persistencia.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Categoria = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NombreCompleto = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CreditoHabilitado = table.Column<bool>(type: "INTEGER", nullable: false),
                    MontoCredito = table.Column<decimal>(type: "TEXT", precision: 12, scale: 2, nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionesSistema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Clave = table.Column<int>(type: "INTEGER", nullable: false),
                    Valor = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionesSistema", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiasLaborales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaCierre = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EstaCerrado = table.Column<bool>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiasLaborales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maquinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identificador = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EstaOcupada = table.Column<bool>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Numero = table.Column<int>(type: "INTEGER", nullable: false),
                    Ocupada = table.Column<bool>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                    NombreUsuario = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Rol = table.Column<int>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VentasConfiteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Total = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasConfiteria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Precio = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    StockMinimo = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    CatalogoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Catalogos_CatalogoId",
                        column: x => x.CatalogoId,
                        principalTable: "Catalogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Amonestaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmpleadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Gravedad = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Duracion = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amonestaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Amonestaciones_Usuarios_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiaLaboralId = table.Column<int>(type: "INTEGER", nullable: false),
                    TitularId = table.Column<int>(type: "INTEGER", nullable: false),
                    AuxiliarId = table.Column<int>(type: "INTEGER", nullable: true),
                    Salida = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TotalMaquinas = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TotalMesas = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    TotalConfiteria = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    TotalDeuda = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    MontoEsperado = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    MontoContado = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    Diferencia = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    GravedadDiferencia = table.Column<int>(type: "INTEGER", nullable: false),
                    NotaCierre = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turnos_DiasLaborales_DiaLaboralId",
                        column: x => x.DiaLaboralId,
                        principalTable: "DiasLaborales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turnos_Usuarios_AuxiliarId",
                        column: x => x.AuxiliarId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Turnos_Usuarios_TitularId",
                        column: x => x.TitularId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemsConfiteria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    VentaConfiteriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsConfiteria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsConfiteria_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsConfiteria_VentasConfiteria_VentaConfiteriaId",
                        column: x => x.VentaConfiteriaId,
                        principalTable: "VentasConfiteria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Anticipos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmpleadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioAutorizanteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    ForzadoPorJefe = table.Column<bool>(type: "INTEGER", nullable: false),
                    TurnoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
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
                name: "Cuentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    TurnoId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpleadoAperturaId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpleadoCierreId = table.Column<int>(type: "INTEGER", nullable: true),
                    VentaConfiteriaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Total = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuentas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cuentas_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cuentas_Usuarios_EmpleadoAperturaId",
                        column: x => x.EmpleadoAperturaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cuentas_Usuarios_EmpleadoCierreId",
                        column: x => x.EmpleadoCierreId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cuentas_VentasConfiteria_VentaConfiteriaId",
                        column: x => x.VentaConfiteriaId,
                        principalTable: "VentasConfiteria",
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
                    EmpleadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    NombreEvento = table.Column<int>(type: "INTEGER", nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    FechaRevisado = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Revisado = table.Column<bool>(type: "INTEGER", nullable: false),
                    NotaRevision = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    UsuarioRevisionId = table.Column<int>(type: "INTEGER", nullable: true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventosTurno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventosTurno_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventosTurno_Usuarios_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventosTurno_Usuarios_UsuarioRevisionId",
                        column: x => x.UsuarioRevisionId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngresosStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TurnoId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpleadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresosStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngresosStock_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IngresosStock_Usuarios_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosTurnoEmpleado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmpleadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    TurnoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Salida = table.Column<DateTime>(type: "TEXT", nullable: true),
                    HorasTrabajadas = table.Column<decimal>(type: "TEXT", precision: 5, scale: 2, nullable: true),
                    Limpieza = table.Column<bool>(type: "INTEGER", nullable: false),
                    Comisiones = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    Descuentos = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosTurnoEmpleado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosTurnoEmpleado_Turnos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegistrosTurnoEmpleado_Usuarios_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CuentaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Metodo = table.Column<int>(type: "INTEGER", nullable: false),
                    PagoParcial = table.Column<bool>(type: "INTEGER", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    CuentaBaseId = table.Column<int>(type: "INTEGER", nullable: true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagos_Cuentas_CuentaBaseId",
                        column: x => x.CuentaBaseId,
                        principalTable: "Cuentas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pagos_Cuentas_CuentaId",
                        column: x => x.CuentaId,
                        principalTable: "Cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    CuentaId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpleadoResponsableId = table.Column<int>(type: "INTEGER", nullable: false),
                    DescripcionPrenda = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    MontoPrenda = table.Column<double>(type: "REAL", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Estado = table.Column<int>(type: "INTEGER", maxLength: 20, nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prendas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prendas_Cuentas_CuentaId",
                        column: x => x.CuentaId,
                        principalTable: "Cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prendas_Usuarios_EmpleadoResponsableId",
                        column: x => x.EmpleadoResponsableId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SesionesMaquina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaquinaId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TotalIngresos = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalEgresos = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionesMaquina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SesionesMaquina_Cuentas_Id",
                        column: x => x.Id,
                        principalTable: "Cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionesMaquina_Maquinas_MaquinaId",
                        column: x => x.MaquinaId,
                        principalTable: "Maquinas",
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
                    FechaFin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MontoSesionMesa = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionesMesa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SesionesMesa_Cuentas_Id",
                        column: x => x.Id,
                        principalTable: "Cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionesMesa_Mesas_MesaId",
                        column: x => x.MesaId,
                        principalTable: "Mesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemsIngresoStock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IngresoStockId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Cantidad = table.Column<int>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsIngresoStock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsIngresoStock_IngresosStock_IngresoStockId",
                        column: x => x.IngresoStockId,
                        principalTable: "IngresosStock",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemsIngresoStock_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CobrosDeudas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PrendaId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpleadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Nota = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CobrosDeudas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CobrosDeudas_Prendas_PrendaId",
                        column: x => x.PrendaId,
                        principalTable: "Prendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CobrosDeudas_Usuarios_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransaccionesMaquina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Monto = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    EsIngreso = table.Column<bool>(type: "INTEGER", nullable: false),
                    SesionId = table.Column<int>(type: "INTEGER", nullable: false),
                    Activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionesMaquina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransaccionesMaquina_SesionesMaquina_SesionId",
                        column: x => x.SesionId,
                        principalTable: "SesionesMaquina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amonestaciones_EmpleadoId",
                table: "Amonestaciones",
                column: "EmpleadoId");

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
                name: "IX_CobrosDeudas_EmpleadoId",
                table: "CobrosDeudas",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CobrosDeudas_PrendaId",
                table: "CobrosDeudas",
                column: "PrendaId");

            migrationBuilder.CreateIndex(
                name: "IX_ConfiguracionesSistema_Clave",
                table: "ConfiguracionesSistema",
                column: "Clave",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_ClienteId",
                table: "Cuentas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_EmpleadoAperturaId",
                table: "Cuentas",
                column: "EmpleadoAperturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_EmpleadoCierreId",
                table: "Cuentas",
                column: "EmpleadoCierreId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_TurnoId",
                table: "Cuentas",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_VentaConfiteriaId",
                table: "Cuentas",
                column: "VentaConfiteriaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventosTurno_EmpleadoId",
                table: "EventosTurno",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosTurno_TurnoId",
                table: "EventosTurno",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_EventosTurno_UsuarioRevisionId",
                table: "EventosTurno",
                column: "UsuarioRevisionId");

            migrationBuilder.CreateIndex(
                name: "IX_IngresosStock_EmpleadoId",
                table: "IngresosStock",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_IngresosStock_TurnoId",
                table: "IngresosStock",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsConfiteria_ProductoId",
                table: "ItemsConfiteria",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsConfiteria_VentaConfiteriaId",
                table: "ItemsConfiteria",
                column: "VentaConfiteriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsIngresoStock_IngresoStockId",
                table: "ItemsIngresoStock",
                column: "IngresoStockId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsIngresoStock_ProductoId",
                table: "ItemsIngresoStock",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinas_Identificador",
                table: "Maquinas",
                column: "Identificador",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_Numero",
                table: "Mesas",
                column: "Numero",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_CuentaBaseId",
                table: "Pagos",
                column: "CuentaBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_CuentaId",
                table: "Pagos",
                column: "CuentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Prendas_ClienteId",
                table: "Prendas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Prendas_CuentaId",
                table: "Prendas",
                column: "CuentaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prendas_EmpleadoResponsableId",
                table: "Prendas",
                column: "EmpleadoResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CatalogoId",
                table: "Productos",
                column: "CatalogoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosTurnoEmpleado_EmpleadoId",
                table: "RegistrosTurnoEmpleado",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosTurnoEmpleado_TurnoId",
                table: "RegistrosTurnoEmpleado",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMaquina_MaquinaId",
                table: "SesionesMaquina",
                column: "MaquinaId");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesMesa_MesaId",
                table: "SesionesMesa",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesMaquina_SesionId",
                table: "TransaccionesMaquina",
                column: "SesionId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_AuxiliarId",
                table: "Turnos",
                column: "AuxiliarId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_DiaLaboralId",
                table: "Turnos",
                column: "DiaLaboralId");

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_TitularId",
                table: "Turnos",
                column: "TitularId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amonestaciones");

            migrationBuilder.DropTable(
                name: "Anticipos");

            migrationBuilder.DropTable(
                name: "CobrosDeudas");

            migrationBuilder.DropTable(
                name: "ConfiguracionesSistema");

            migrationBuilder.DropTable(
                name: "EventosTurno");

            migrationBuilder.DropTable(
                name: "ItemsConfiteria");

            migrationBuilder.DropTable(
                name: "ItemsIngresoStock");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "RegistrosTurnoEmpleado");

            migrationBuilder.DropTable(
                name: "SesionesMesa");

            migrationBuilder.DropTable(
                name: "TransaccionesMaquina");

            migrationBuilder.DropTable(
                name: "Prendas");

            migrationBuilder.DropTable(
                name: "IngresosStock");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Mesas");

            migrationBuilder.DropTable(
                name: "SesionesMaquina");

            migrationBuilder.DropTable(
                name: "Catalogos");

            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "Maquinas");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "VentasConfiteria");

            migrationBuilder.DropTable(
                name: "DiasLaborales");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
