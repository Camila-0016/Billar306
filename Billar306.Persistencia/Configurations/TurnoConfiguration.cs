using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Billar306.Dominio.Models.Operatividad;

namespace Billar306.Persistencia.Configurations
{
    public class TurnoConfiguration : EntidadBaseConfiguration<Turno>
    {
        public override void Configure(EntityTypeBuilder<Turno> builder)
        {
            base.Configure(builder);
            builder.ToTable("Turnos");

            // Precisión para métricas financieras
            builder.Property(t => t.TotalMaquinas).HasPrecision(18, 2).IsRequired();
            builder.Property(t => t.TotalMesas).HasPrecision(18, 2);
            builder.Property(t => t.TotalConfiteria).HasPrecision(18, 2);
            builder.Property(t => t.TotalDeuda).HasPrecision(18, 2);
            builder.Property(t => t.MontoEsperado).HasPrecision(18, 2);
            builder.Property(t => t.MontoContado).HasPrecision(18, 2);
            builder.Property(t => t.Diferencia).HasPrecision(18, 2);

            builder.Property(t => t.NotaCierre).HasMaxLength(1000);

            // Relación con DiaLaboral
            builder.HasOne(t => t.DiaLaboral)
                   .WithMany(d => d.Turnos)
                   .HasForeignKey(t => t.DiaLaboralId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación con Usuario (Titular)
            builder.HasOne(t => t.Titular)
                   .WithMany(u => u.Turnos)
                   .HasForeignKey(t => t.TitularId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación con Usuario (Auxiliar)
            builder.HasOne(t => t.Auxiliar)
                   .WithMany()
                   .HasForeignKey(t => t.AuxiliarId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
