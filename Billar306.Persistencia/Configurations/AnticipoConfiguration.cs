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
    public class AnticipoConfiguration : EntidadBaseConfiguration<Anticipo>
    {
        public override void Configure(EntityTypeBuilder<Anticipo> builder)
        {
            base.Configure(builder);
            builder.ToTable("Anticipos");

            builder.Property(a => a.Monto)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.HasOne(a => a.Empleado)
                   .WithMany(u => u.Anticipos)
                   .HasForeignKey(a => a.EmpleadoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.UsuarioAutorizante)
                   .WithMany()
                   .HasForeignKey(a => a.UsuarioAutorizanteId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
