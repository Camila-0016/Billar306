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
    public class PrendaConfiguration : EntidadBaseConfiguration<Prenda>
    {
        public override void Configure(EntityTypeBuilder<Prenda> builder)
        {
            base.Configure(builder);

            builder.ToTable("Prendas");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.DescripcionPrenda)
                  .IsRequired()
                  .HasMaxLength(500);

            builder.Property(e => e.Estado)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(e => e.MontoPrenda)
                  .HasConversion<double>();

            // Relación con Cliente
            builder.HasOne(p => p.Cliente)
                  .WithMany() // Cambiar a .WithMany(c => c.Prendas) si Cliente tiene la colección
                  .HasForeignKey(p => p.ClienteId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relación con Usuario (EmpleadoResponsable)
            builder.HasOne(p => p.EmpleadoResponsable)
                  .WithMany()
                  .HasForeignKey(p => p.EmpleadoResponsableId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relación con CuentaBase
            builder.HasOne(p => p.Cuenta)
                  .WithOne(c => c.PrendaGenerada)
                  .HasForeignKey<Prenda>(p => p.CuentaId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relación con CobroDeuda (Abonos)
            builder.HasMany(p => p.Abonos)
                  .WithOne(a => a.Prenda)
                  .HasForeignKey(a => a.PrendaId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
