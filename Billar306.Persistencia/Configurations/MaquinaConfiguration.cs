using Billar306.Dominio.Models.Venta.Maquina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Persistencia.Configurations
{
    public class MaquinaConfiguration : EntidadBaseConfiguration<Maquina>
    {
        public override void Configure(EntityTypeBuilder<Maquina> builder)
        {
            base.Configure(builder);
            builder.ToTable("Maquinas");

            builder.Property(m => m.Identificador)
                   .HasMaxLength(50)
                   .IsRequired();
            // Índice único: No pueden existir dos máquinas con el mismo identificador
            builder.HasIndex(m => m.Identificador).IsUnique();

            builder.Property(m => m.EstaOcupada).IsRequired();
        }
    }
}
