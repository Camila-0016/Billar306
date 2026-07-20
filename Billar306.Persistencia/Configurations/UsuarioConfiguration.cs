using Billar306.Dominio.Models.Clientes;
using Billar306.Dominio.Models.Control;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Billar306.Persistencia.Configurations
{
    public class UsuarioConfiguration : EntidadBaseConfiguration<Usuario>
    {
        public override void Configure(EntityTypeBuilder<Usuario> builder)
        {
            base.Configure(builder);
            builder.ToTable("Usuarios");

            builder.Property(u => u.NombreUsuario)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            builder.Property(u => u.Rol)
                   .IsRequired();
        }
    }
}
