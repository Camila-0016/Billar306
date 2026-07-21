using Billar306.Dominio.Models.Clientes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Billar306.Persistencia.Configurations
{
    public class ClienteConfiguration : EntidadBaseConfiguration<Cliente>
    {
        public override void Configure(EntityTypeBuilder<Cliente> builder)
        {
            base.Configure(builder);

            builder.ToTable("Clientes");

            builder.Property(c => c.NombreCompleto)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.CreditoHabilitado)
                   .IsRequired();

            builder.Property(c => c.MontoCredito)
                   .HasPrecision(12, 2)
                   .IsRequired();
        }
    }
}