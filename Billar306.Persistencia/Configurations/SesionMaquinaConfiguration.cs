using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Billar306.Dominio.Models.Venta.Maquina;

namespace Billar306.Persistencia.Configurations
{
    public class SesionMaquinaConfiguration : IEntityTypeConfiguration<SesionMaquina>
    {
        // Nota: NO hereda de EntidadBaseConfiguration porque los campos base (Id, Activo) 
        // ya fueron configurados en CuentaBaseConfiguration.
        public void Configure(EntityTypeBuilder<SesionMaquina> builder)
        {
            // Define tabla propia para la subclase (Table-Per-Type)
            builder.ToTable("SesionesMaquina");

            builder.HasOne(s => s.Maquina)
                   .WithMany(m => m.Sesiones)
                   .HasForeignKey(s => s.MaquinaId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
