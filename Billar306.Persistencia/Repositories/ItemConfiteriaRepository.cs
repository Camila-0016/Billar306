using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Venta;

namespace Billar306.Persistencia.Repositories
{
    public class ItemConfiteriaRepository : Repository<ItemConfiteria>, IItemConfiteriaRepository
    {
        public ItemConfiteriaRepository(AppDbContext context) : base(context) { }
    }
}