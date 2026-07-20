using Billar306.Dominio.Models.Venta;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Persistencia.Configurations
{
    public class VentaConfiteriaConfiguration : EntidadBaseConfiguration<VentaConfiteria>
    {
        public override void Configure(EntityTypeBuilder<VentaConfiteria> builder)
        {
            base.Configure(builder);
            builder.ToTable("VentasConfiteria");

            builder.Property(v => v.Total)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // La relación con CuentaBase ya fue establecida en CuentaBaseConfiguration
        }
    }
}
