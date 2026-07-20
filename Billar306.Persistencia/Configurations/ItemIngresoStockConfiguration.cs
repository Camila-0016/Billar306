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
    public class ItemIngresoStockConfiguration : EntidadBaseConfiguration<ItemIngresoStock>
    {
        public override void Configure(EntityTypeBuilder<ItemIngresoStock> builder)
        {
            base.Configure(builder);

            builder.ToTable("ItemsIngresoStock");

            builder.Property(i => i.Cantidad)
                   .IsRequired();

            // Relación interna con la Cabecera (Composición)
            builder.HasOne(i => i.IngresoStock)
                   .WithMany(ing => ing.Productos)
                   .HasForeignKey(i => i.IngresoStockId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación externa con Producto
            builder.HasOne(i => i.Producto)
                   .WithMany() // Unidireccional: Producto no necesita lista de los ingresos en los que participó
                   .HasForeignKey(i => i.ProductoId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
