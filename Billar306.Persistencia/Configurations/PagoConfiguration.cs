using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Billar306.Dominio.Models.Venta;

namespace Billar306.Persistencia.Configurations
{
    public class PagoConfiguration : EntidadBaseConfiguration<Pago>
    {
        public override void Configure(EntityTypeBuilder<Pago> builder)
        {
            base.Configure(builder);
            builder.ToTable("Pagos");

            builder.Property(p => p.Monto)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(p => p.Metodo).IsRequired();

            builder.HasOne(p => p.Cuenta)
                   .WithMany() // O ajusta si CuentaBase tiene una colección de Pagos
                   .HasForeignKey(p => p.CuentaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
