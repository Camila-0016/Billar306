namespace Billar306.API.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public decimal SueldoBase { get; set; }
        public bool Activo { get; set; }
    }
}