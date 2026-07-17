using Billar306.Data.Models.Clientes;
using Billar306.Data.Models.Control;
using Billar306.Data.Models.Empleado;
using Billar306.Data.Models.Operatividad;
using Billar306.Data.Models.Venta;
using Billar306.Data.Models.Venta.Mesa;
using Microsoft.EntityFrameworkCore;

namespace Billar306.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tablas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<SesionMesa> SesionesMesa { get; set; }
        public DbSet<ItemConfiteria> ItemsConfiteria { get; set; }
        public DbSet<ConsumicionMesa> ConsumicionesMesa { get; set; }
        public DbSet<ClienteFrecuente> ClientesFrecuentes { get; set; }
        public DbSet<Fiado> Fiados { get; set; }
        public DbSet<Anticipo> Anticipos { get; set; }
        public DbSet<EventoTurno> EventosTurno { get; set; }
        public DbSet<ConfiguracionSistema> ConfiguracionSistema { get; set; }
        public DbSet<RegistroHoraEmpleado> RegistrosHora { get; set; }
        public DbSet<IngresoStock> IngresosStock { get; set; }
        public DbSet<VentaDirecta> VentasDirectas { get; set; }
        public DbSet<AbonoFiado> AbonosFiado { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Usuario ---
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario)
                .IsUnique();

            // --- Anticipo: dos FK a Usuario, hay que decirle a EF cuál es cuál ---
            modelBuilder.Entity<Anticipo>()
                .HasOne(a => a.Empleado)
                .WithMany(u => u.Anticipos)
                .HasForeignKey(a => a.EmpleadoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Anticipo>()
                .HasOne(a => a.UsuarioAutorizante)
                .WithMany()
                .HasForeignKey(a => a.UsuarioAutorizanteId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- SesionMesa: FK a Usuario ---
            modelBuilder.Entity<SesionMesa>()
                .HasOne(s => s.Usuario)
                .WithMany()
                .HasForeignKey(s => s.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Fiado: FK a Usuario (registro) ---
            modelBuilder.Entity<Fiado>()
                .HasOne(f => f.UsuarioRegistro)
                .WithMany()
                .HasForeignKey(f => f.UsuarioRegistroId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Fiado relacion con SesionMesa---
            modelBuilder.Entity<Fiado>()
                .HasOne(f => f.SesionMesa)
                .WithOne(s => s.Fiado)
                .HasForeignKey<Fiado>(f => f.SesionMesaId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            //Abono de fiado
            modelBuilder.Entity<AbonoFiado>()
    .HasOne(a => a.Fiado)
    .WithMany(f => f.Abonos)
    .HasForeignKey(a => a.FiadoId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AbonoFiado>()
                .HasOne(a => a.Usuario)
                .WithMany()
                .HasForeignKey(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ingreso durante el turno
            modelBuilder.Entity<IngresoStock>()
    .HasOne(i => i.Usuario)
    .WithMany()
    .HasForeignKey(i => i.UsuarioId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IngresoStock>()
                .HasOne(i => i.Turno)
                .WithMany()
                .HasForeignKey(i => i.TurnoId)
                .OnDelete(DeleteBehavior.Restrict);

            //Horas empleado
            modelBuilder.Entity<RegistroHoraEmpleado>()
    .HasOne(r => r.Usuario)
    .WithMany()
    .HasForeignKey(r => r.UsuarioId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RegistroHoraEmpleado>()
                .HasOne(r => r.Turno)
                .WithMany()
                .HasForeignKey(r => r.TurnoId)
                .OnDelete(DeleteBehavior.Restrict);

            //Venta Confiteria
            modelBuilder.Entity<VentaDirecta>()
    .HasOne(v => v.Usuario)
    .WithMany()
    .HasForeignKey(v => v.UsuarioId)
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VentaDirecta>()
                .HasOne(v => v.Turno)
                .WithMany()
                .HasForeignKey(v => v.TurnoId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- EventoTurno: FK a Usuario ---
            modelBuilder.Entity<EventoTurno>()
                .HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // --- ConfiguracionSistema: clave única ---
            modelBuilder.Entity<ConfiguracionSistema>()
                .HasIndex(c => c.Clave)
                .IsUnique();

            // --- Datos iniciales: Mesas ---
            modelBuilder.Entity<Mesa>().HasData(
                Enumerable.Range(1, 8).Select(i => new Mesa
                {
                    Id = i,
                    Numero = i,
                    Estado = "Libre"
                }).ToArray()
            );

            // --- Datos iniciales: Configuración ---
            modelBuilder.Entity<ConfiguracionSistema>().HasData(
                new ConfiguracionSistema { Id = 1, Clave = "LimiteAnticipoPorc", Valor = "40", Descripcion = "Porcentaje máximo de anticipo sobre sueldo" },
                new ConfiguracionSistema { Id = 2, Clave = "MontoMaxGravedadBaja", Valor = "2000", Descripcion = "Diferencia de caja máxima para gravedad baja" },
                new ConfiguracionSistema { Id = 3, Clave = "PeriodoRedondeo", Valor = "30", Descripcion = "Días del período de redondeo a favor del cliente" },
                new ConfiguracionSistema { Id = 4, Clave = "PausaMinimaJuegoMin", Valor = "30", Descripcion = "Minutos mínimos entre sesiones de juego del mismo empleado" },
                new ConfiguracionSistema { Id = 5, Clave = "ModalidadJuego", Valor = "consumicion", Descripcion = "Modalidad de juego: tiempo | consumicion | ambas" },
                new ConfiguracionSistema { Id = 6, Clave = "DuracionTokenHoras", Valor = "8", Descripcion = "Horas de validez del token JWT" },
                new ConfiguracionSistema { Id = 7, Clave = "TarifaHoraEmpleado", Valor = "0", Descripcion = "Tarifa por hora para empleados" },
                new ConfiguracionSistema { Id = 8, Clave = "TarifaHoraEncargado", Valor = "0", Descripcion = "Tarifa por hora para encargados" },
                new ConfiguracionSistema { Id = 9, Clave = "TarifaHoraMesa", Valor = "0", Descripcion = "Precio por hora de mesa" },
new ConfiguracionSistema { Id = 10, Clave = "RecargoPorcentajeNocturno", Valor = "50", Descripcion = "Porcentaje de recargo nocturno (después de las 6am)" },
new ConfiguracionSistema { Id = 11, Clave = "HoraInicioRecargo", Valor = "6", Descripcion = "Hora desde la que aplica el recargo (formato 24h)" },
new ConfiguracionSistema { Id = 12, Clave = "HoraFinRecargo", Valor = "14", Descripcion = "Hora hasta la que aplica el recargo (formato 24h)" }
                );


            // --- Datos iniciales: Usuario jefe por defecto ---
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    NombreUsuario = "jefe",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin1234"),
                    Rol = "jefe",
                    NombreCompleto = "Administrador",
                    SueldoBase = 0,
                    Activo = true,
                    FechaCreacion = new DateTime(2026, 1, 1)
                }
            );
        }
    }
}