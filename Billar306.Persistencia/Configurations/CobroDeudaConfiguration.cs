using Billar306.Dominio.Models.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Persistencia.Configurations
{
    public class CobroDeudaConfiguration : EntidadBaseConfiguration<CobroDeuda>
    {
        public override void Configure(EntityTypeBuilder<CobroDeuda> builder)
        {
            // 1. Ejecución obligatoria de la configuración base
            base.Configure(builder);

            builder.ToTable("CobrosDeudas");

            // 2. Configuración de Propiedades
            builder.Property(e => e.Nota)
                  .HasMaxLength(500)
                  .IsRequired(false);

            // 3. Precisión estricta para valores financieros (sin conversión a double)
            builder.Property(e => e.Monto)
                  .HasPrecision(18, 2)
                  .IsRequired();

            // 4. Configuración de Relaciones
            builder.HasOne(c => c.Prenda)
                  .WithMany(p => p.Abonos)
                  .HasForeignKey(c => c.PrendaId)
                  .OnDelete(DeleteBehavior.Restrict); // Impide eliminar una Prenda con abonos existentes

            builder.HasOne(c => c.Empleado)
                  .WithMany()
                  .HasForeignKey(c => c.EmpleadoId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
