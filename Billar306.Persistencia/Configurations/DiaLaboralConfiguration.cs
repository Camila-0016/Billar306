using Billar306.Dominio.Models.Operatividad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billar306.Persistencia.Configurations
{
    public class DiaLaboralConfiguration : EntidadBaseConfiguration<DiaLaboral>
    {
        public override void Configure(EntityTypeBuilder<DiaLaboral> builder)
        {
            base.Configure(builder);
            builder.ToTable("DiasLaborales");

            builder.Property(d => d.EstaCerrado).IsRequired();
        }
    }
}
