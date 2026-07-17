using Billar306.API.Repositories;
using Billar306.Data.Models.Control;

namespace Billar306.API.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepo;

        public UsuarioService(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
            => await _usuarioRepo.ObtenerTodosAsync();

        public async Task<(bool ok, string error)> CrearUsuarioAsync(
            string nombreUsuario, string password, string rol, string nombreCompleto, decimal sueldoBase)
        {
            var existe = await _usuarioRepo.ObtenerPorNombreUsuarioAsync(nombreUsuario);
            if (existe != null) return (false, "Ya existe un usuario con ese nombre.");

            var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Rol = rol,
                NombreCompleto = nombreCompleto,
                SueldoBase = sueldoBase,
                Activo = true,
                FechaCreacion = DateTime.Now
            };

            await _usuarioRepo.AgregarAsync(usuario);
            await _usuarioRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }

        public async Task<(bool ok, string error)> DesactivarUsuarioAsync(int id)
        {
            var usuario = await _usuarioRepo.ObtenerPorIdAsync(id);
            if (usuario == null) return (false, "Usuario no encontrado.");

            usuario.Activo = false;
            await _usuarioRepo.GuardarCambiosAsync();
            return (true, string.Empty);
        }
    }
}