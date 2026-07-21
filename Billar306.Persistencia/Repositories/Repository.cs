using Billar306.API.Data;
using Billar306.Dominio.Interfaces;
using Billar306.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Billar306.Persistencia.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntidadBase
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> ObtenerPorIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> ObtenerTodosAsync()
            => await _dbSet.ToListAsync();

        public async Task<T> AgregarAsync(T entidad)
        {
            await _dbSet.AddAsync(entidad);
            await _context.SaveChangesAsync();
            return entidad;
        }

        public async Task ActualizarAsync(T entidad)
        {
            _dbSet.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(T entidad)
        {
            entidad.Activo = false;
            _dbSet.Update(entidad);
            await _context.SaveChangesAsync();
        }
    }
}