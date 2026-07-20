using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Billar306.Dominio.Models.Venta.Maquina;

namespace Billar306.Persistencia.Configurations
{
    public class TransaccionMaquinaConfiguration : EntidadBaseConfiguration<TransaccionMaquina>
    {
        public override void Configure(EntityTypeBuilder<TransaccionMaquina> builder)
        {
            base.Configure(builder);
            builder.ToTable("TransaccionesMaquina");

            builder.Property(t => t.Monto)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(t => t.EsIngreso).IsRequired();

            builder.HasOne(t => t.Sesion)
                   .WithMany(s => s.Transacciones)
                   .HasForeignKey(t => t.SesionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
