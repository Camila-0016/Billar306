using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Billar306.Dominio.Models.Venta;

namespace Billar306.Persistencia.Configurations
{
    public class CuentaBaseConfiguration : EntidadBaseConfiguration<CuentaBase>
    {
        public override void Configure(EntityTypeBuilder<CuentaBase> builder)
        {
            base.Configure(builder);
            builder.ToTable("Cuentas");

            builder.Property(c => c.Total)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // Relaciones
            builder.HasOne(c => c.Cliente)
                   .WithMany() // Unidireccional desde Cliente
                   .HasForeignKey(c => c.ClienteId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Turno)
                   .WithMany(t => t.CuentasAbiertas)
                   .HasForeignKey(c => c.TurnoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.EmpleadoApertura)
                   .WithMany()
                   .HasForeignKey(c => c.EmpleadoAperturaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.EmpleadoCierre)
                   .WithMany()
                   .HasForeignKey(c => c.EmpleadoCierreId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación opcional con Confitería
            builder.HasOne(c => c.Confiteria)
                    .WithOne(v => v.CuentaAsociada)
                    .HasForeignKey<CuentaBase>(c => c.VentaConfiteriaId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
