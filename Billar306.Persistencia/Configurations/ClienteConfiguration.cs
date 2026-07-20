using Billar306.Dominio.Models.Clientes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Persistencia.Configurations
{
    public class ClienteConfiguration : EntidadBaseConfiguration<Cliente>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Cliente> builder)
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
