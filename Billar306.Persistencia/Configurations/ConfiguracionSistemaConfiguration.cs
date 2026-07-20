using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Billar306.Dominio.Models.Control;

namespace Billar306.Persistencia.Configurations
{
    public class ConfiguracionSistemaConfiguration : EntidadBaseConfiguration<ConfiguracionSistema>
    {
        public override void Configure(EntityTypeBuilder<ConfiguracionSistema> builder)
        {
            // 1. Configuración base
            base.Configure(builder);

            builder.ToTable("ConfiguracionesSistema");

            // 2. Restricción de unicidad: Un mismo parámetro no puede existir dos veces
            builder.HasIndex(c => c.Clave)
                   .IsUnique();

            builder.Property(c => c.Clave)
                   .IsRequired();

            // 3. Configuración estricta de decimales para tarifas
            builder.Property(c => c.Valor)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // 4. Límite de longitud para evitar almacenamiento innecesario
            builder.Property(c => c.Descripcion)
                   .HasMaxLength(255);
        }
    }
}
