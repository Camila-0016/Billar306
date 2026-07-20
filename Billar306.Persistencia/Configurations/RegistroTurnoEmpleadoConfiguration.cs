using Billar306.Dominio.Models.Empleado;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billar306.Persistencia.Configurations
{
    public class RegistroTurnoEmpleadoConfiguration : EntidadBaseConfiguration<RegistroTurnoEmpleado>
    {
        public override void Configure(EntityTypeBuilder<RegistroTurnoEmpleado> builder)
        {
            base.Configure(builder);
            builder.ToTable("RegistrosTurnoEmpleado");

            builder.Property(r => r.HorasTrabajadas).HasPrecision(5, 2);
            builder.Property(r => r.Comisiones).HasPrecision(18, 2);
            builder.Property(r => r.Descuentos).HasPrecision(18, 2);

            builder.HasOne(r => r.Empleado)
                   .WithMany()
                   .HasForeignKey(r => r.EmpleadoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Turno)
                   .WithMany()
                   .HasForeignKey(r => r.TurnoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
