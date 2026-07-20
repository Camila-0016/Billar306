using Billar306.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billar306.Persistencia.Configurations
{
    public abstract class EntidadBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : EntidadBase
    {
        public virtual void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<T> builder)
        {
            // Clave Primaria
            builder.HasKey(e => e.Id);

            // Filtro Global de baja lógica
            builder.HasQueryFilter(e => e.Activo);

            // Protección de la Fecha de Creación
            builder.Property(e => e.FechaInicio)
                   .IsRequired()
                   .ValueGeneratedOnAdd();
        }
    }
}
