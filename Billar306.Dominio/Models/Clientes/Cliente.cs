using Billar306.Dominio.Models.Venta;

namespace Billar306.Dominio.Models.Clientes
{
    public class Cliente : EntidadBase
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public bool CreditoHabilitado { get; set; } = false;
        public decimal MontoCredito { get; set; } 

    }
}