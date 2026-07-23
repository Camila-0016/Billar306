using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models.Control;

namespace Billar306.Persistencia.Repositories
{
    public class CatalogoRepository : Repository<Catalogo>, ICatalogoRepository
    {
        public CatalogoRepository(AppDbContext context) : base(context) { }
    }
}