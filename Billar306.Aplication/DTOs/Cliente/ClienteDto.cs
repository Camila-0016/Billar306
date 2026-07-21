using System;

namespace Billar306.Aplicacion.DTOs.Clientes
{
    public record ClienteDto(int Id, string NombreCompleto, bool Activo, DateTime FechaInicio);
}