using Billar306.Aplicacion.DTOs.Usuarios;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Control;
using BCrypt.Net;

namespace Billar306.Aplicacion.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioDto>> ObtenerTodosAsync()
        {
            var usuarios = await _usuarioRepository.ObtenerTodosAsync();
            return usuarios.Select(MapearADto).ToList();
        }

        public async Task<UsuarioDto?> ObtenerPorIdAsync(int id)
        {
            var usuario = await _usuarioRepository.ObtenerPorIdAsync(id);
            return usuario is null ? null : MapearADto(usuario);
        }

        public async Task<(bool Exito, string? Error, UsuarioDto? Usuario)> AgregarAsync(CrearUsuarioDto dto)
        {
            var existente = await _usuarioRepository.BuscarPorNombreUsuarioAsync(dto.NombreUsuario);
            if (existente is not null)
                return (false, "Ya existe un usuario con ese nombre de usuario.", null);

            var nuevoUsuario = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Rol = dto.Rol
            };

            await _usuarioRepository.AgregarAsync(nuevoUsuario);
            return (true, null, MapearADto(nuevoUsuario));
        }

        public async Task<bool> ActualizarAsync(int id, ActualizarUsuarioDto dto)
        {
            var usuarioExistente = await _usuarioRepository.ObtenerPorIdAsync(id);
            if (usuarioExistente is null) return false;

            usuarioExistente.NombreUsuario = dto.NombreUsuario;
            usuarioExistente.Rol = dto.Rol;
            usuarioExistente.Activo = dto.Activo;

            await _usuarioRepository.ActualizarAsync(usuarioExistente);
            return true;
        }

        private static UsuarioDto MapearADto(Usuario u)
            => new(u.Id, u.NombreUsuario, u.Rol, u.Activo, u.FechaInicio);
    }
}