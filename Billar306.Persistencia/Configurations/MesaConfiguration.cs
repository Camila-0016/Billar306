using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Billar306.Dominio.Models.Venta.Mesas;

namespace Billar306.Persistencia.Configurations
{
    public class MesaConfiguration : EntidadBaseConfiguration<Mesa>
    {
        public override void Configure(EntityTypeBuilder<Mesa> builder)
        {
            base.Configure(builder);
            builder.ToTable("Mesas");

            builder.Property(m => m.Numero).IsRequired();

            // Índice único: No pueden existir dos mesas con el mismo número
            builder.HasIndex(m => m.Numero).IsUnique();
        }
    }
}
