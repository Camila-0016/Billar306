using Billar306.Dominio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billar306.Dominio.Interfaces
{
    public interface IRepository<T> where T : EntidadBase
    {
        Task<T?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<T> AgregarAsync(T entidad);
        Task ActualizarAsync(T entidad);
        Task EliminarAsync(T entidad);
    }
}