using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Billar306.Dominio.Models.Venta;

namespace Billar306.Persistencia.Configurations
{
    public class ItemConfiteriaConfiguration : EntidadBaseConfiguration<ItemConfiteria>
    {
        public override void Configure(EntityTypeBuilder<ItemConfiteria> builder)
        {
            base.Configure(builder);
            builder.ToTable("ItemsConfiteria");

            builder.Property(i => i.Nombre)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(i => i.Cantidad)
                   .IsRequired();

            builder.Property(i => i.PrecioUnitario)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(i => i.Total)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // Relación con la Cabecera (Composición)
            builder.HasOne(i => i.Venta)
                   .WithMany(v => v.ItemsConfiterias)
                   .HasForeignKey(i => i.VentaConfiteriaId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación con el Producto
            builder.HasOne(i => i.Producto)
                   .WithMany() // Unidireccional
                   .HasForeignKey(i => i.ProductoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
