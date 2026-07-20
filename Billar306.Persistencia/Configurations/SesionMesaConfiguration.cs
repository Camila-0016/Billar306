using Billar306.Dominio.Models.Venta.Maquina;
using Billar306.Dominio.Models.Venta.Mesas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billar306.Persistencia.Configurations
{
    public class SesionMesaConfiguration : IEntityTypeConfiguration<SesionMesa>
    {
        public void Configure(EntityTypeBuilder<SesionMesa> builder)
        {
            builder.ToTable("SesionesMesa");

            builder.Property(s => s.MontoSesionMesa)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // Relación
            builder.HasOne(s => s.Mesa)
                   .WithMany(m => m.Sesiones)
                   .HasForeignKey(s => s.MesaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
