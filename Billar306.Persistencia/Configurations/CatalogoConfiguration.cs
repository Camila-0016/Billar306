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
    public class CatalogoConfiguration : EntidadBaseConfiguration<Catalogo>
    {
        public override void Configure(EntityTypeBuilder<Catalogo> builder)
        {
            // 1. Configuración base obligatoria
            base.Configure(builder);

            builder.ToTable("Catalogos");

            // 2. Configuración de columnas
            builder.Property(c => c.Categoria)
                   .HasMaxLength(100)
                   .IsRequired();

            // 3. Configuración estricta de la Relación (Protección contra borrado en cascada)
            builder.HasMany(c => c.Productos)
                   .WithOne(p => p.Catalogo) // Se asume que Producto tiene una propiedad de navegación 'Catalogo'
                   .HasForeignKey(p => p.CatalogoId) // Se asume que Producto tiene la clave foránea 'CatalogoId'
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
