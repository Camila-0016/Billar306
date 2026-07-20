using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Billar306.Dominio.Models.Control;

namespace Billar306.Persistencia.Configurations
{
    public class ProductoConfiguration : EntidadBaseConfiguration<Producto>
    {
        public override void Configure(EntityTypeBuilder<Producto> builder)
        {
            // 1. Configuración base obligatoria
            base.Configure(builder);

            builder.ToTable("Productos");

            // 2. Límites estrictos para optimización de memoria
            builder.Property(p => p.Nombre)
                   .HasMaxLength(150)
                   .IsRequired();

            // 3. Precisión obligatoria para valores monetarios
            builder.Property(p => p.Precio)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(p => p.Stock)
                   .IsRequired();

            builder.Property(p => p.StockMinimo)
                   .IsRequired();

            builder.Property(p => p.Descripcion)
                   .HasMaxLength(500);

            // 4. Configuración de la Relación (Protección contra borrado en cascada)
            builder.HasOne(p => p.Catalogo)
                   .WithMany(c => c.Productos)
                   .HasForeignKey(p => p.CatalogoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
