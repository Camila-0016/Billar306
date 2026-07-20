using Billar306.Dominio.Models;
using Billar306.Dominio.Models.Clientes;
using Billar306.Dominio.Models.Control;
using Billar306.Dominio.Models.Empleado;
using Billar306.Dominio.Models.Operatividad;
using Billar306.Dominio.Models.Venta;
using Billar306.Dominio.Models.Venta.Maquina;
using Billar306.Dominio.Models.Venta.Mesas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Billar306.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Tablas
        // 1. Usuarios y Clientes
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ConfiguracionSistema> ConfiguracionesSistema { get; set; }

        // 2. Catálogo y Productos
        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<Producto> Productos { get; set; }

        // 3. Mesas y Máquinas
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Maquina> Maquinas { get; set; }

        // 4. Cuentas y Sesiones (TPT)
        public DbSet<CuentaBase> Cuentas { get; set; }
        public DbSet<SesionMesa> SesionesMesa { get; set; }
        public DbSet<SesionMaquina> SesionesMaquina { get; set; }
        public DbSet<TransaccionMaquina> TransaccionesMaquina { get; set; }

        // 5. Confitería y Stock
        public DbSet<VentaConfiteria> VentasConfiteria { get; set; }
        public DbSet<ItemConfiteria> ItemsConfiteria { get; set; }
        public DbSet<IngresoStock> IngresosStock { get; set; }
        public DbSet<ItemIngresoStock> ItemsIngresoStock { get; set; }

        // 6. Operatividad y Caja
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<DiaLaboral> DiasLaborales { get; set; }
        public DbSet<EventoTurno> EventosTurno { get; set; }
        public DbSet<Anticipo> Anticipos { get; set; }

        // 7. Deudas y Cobros
        public DbSet<Prenda> Prendas { get; set; }
        public DbSet<CobroDeuda> CobrosDeudas { get; set; }

        // 8. Recursos Humanos
        public DbSet<RegistroTurnoEmpleado> RegistrosTurnoEmpleado { get; set; }
        public DbSet<Amonestacion> Amonestaciones { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
            // Protección de campos inmutables (Auditoría)
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntidadBase>())
            {
                if (entry.State == EntityState.Modified)
                {
                    // Evita que la FechaInicio sea alterada en un UPDATE
                    entry.Property(x => x.FechaInicio).IsModified = false;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}