namespace Billar306.API.DTOs
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string TipoEvento { get; set; } = string.Empty;
        public string Gravedad { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public bool Revisado { get; set; }
        public string? NotaRevision { get; set; }
        public string? Usuario { get; set; }
    }
}