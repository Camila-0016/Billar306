using Billar306.Dominio.Models.Operatividad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Persistencia.Configurations
{
    public class EventoTurnoConfiguration : EntidadBaseConfiguration<EventoTurno>
    {
        public override void Configure(EntityTypeBuilder<EventoTurno> builder)
        {
            base.Configure(builder);
            builder.ToTable("EventosTurno");

            builder.Property(e => e.NombreEvento).IsRequired();
            builder.Property(e => e.Descripcion).HasMaxLength(500).IsRequired();
            builder.Property(e => e.NotaRevision).HasMaxLength(500);

            // Relación con Turno
            builder.HasOne(e => e.Turno)
                   .WithMany(t => t.Eventos)
                   .HasForeignKey(e => e.TurnoId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación con Usuario (Empleado implicado)
            builder.HasOne(e => e.Empleado)
                   .WithMany()
                   .HasForeignKey(e => e.EmpleadoId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Relación con Usuario (El que revisó)
            builder.HasOne(e => e.UsuarioRevision)
                   .WithMany()
                   .HasForeignKey(e => e.UsuarioRevisionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
