using Billar306.Dominio.Models.Control;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Persistencia.Configurations
{
    public class IngresoStockConfiguration : EntidadBaseConfiguration<IngresoStock>
    {
        public override void Configure(EntityTypeBuilder<IngresoStock> builder)
        {
            base.Configure(builder);

            builder.ToTable("IngresosStock");

            // Relación con Turno
            builder.HasOne(i => i.TurnoEmpleado)
                   .WithMany(t => t.IngresosStock) // Unidireccional desde Turno
                   .HasForeignKey(i => i.TurnoId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación con Usuario (Empleado)
            builder.HasOne(i => i.Empleado)
                   .WithMany() // Unidireccional o bidireccional dependiendo de tu clase Usuario
                   .HasForeignKey(i => i.EmpleadoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
