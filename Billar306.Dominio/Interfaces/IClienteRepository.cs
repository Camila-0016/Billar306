using Billar306.Dominio.Models.Clientes;
using System.Threading.Tasks;

namespace Billar306.Dominio.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IEnumerable<Cliente>> BuscarPorNombreAsync(string nombre);
        Task<Cliente?> BuscarExactoAsync(string nombre);
    }
}