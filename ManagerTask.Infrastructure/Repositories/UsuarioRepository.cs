using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerTask.Domain.Repositories;
using ManagerTask.Infrastructure.Data;
using ManagerTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagerTask.Infrastructure.Repositories
{
    public class UsuarioRepository : IRepository<Usuario>
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context)
            => _context = context;

        public async Task AddAsync(Usuario entity)
        {
            await _context.Usuarios.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Usuario entity)
        {
            _context.Usuarios.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
            => await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id) ?? new Usuario();


        public async Task<IEnumerable<Usuario>> GetAllAsync()
            => await _context.Usuarios.ToListAsync();


        public async Task RemoveAsync(Usuario entity)
        {
            _context.Usuarios.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
