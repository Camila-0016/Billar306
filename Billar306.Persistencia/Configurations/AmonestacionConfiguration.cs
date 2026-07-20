using Billar306.Dominio.Models.Empleado;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Persistencia.Configurations
{
    public class AmonestacionConfiguration : EntidadBaseConfiguration<Amonestacion>
    {
        public override void Configure(EntityTypeBuilder<Amonestacion> builder)
        {
            base.Configure(builder);
            builder.ToTable("Amonestaciones");

            builder.Property(a => a.Gravedad)
                   .IsRequired();

            builder.Property(a => a.Descripcion)
                   .HasMaxLength(500)
                   .IsRequired();

            builder.Property(a => a.Monto)
                   .HasPrecision(18, 2);

            builder.HasOne(a => a.Empleado)
                   .WithMany(e => e.Amonestaciones)
                   .HasForeignKey(a => a.EmpleadoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
