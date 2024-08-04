using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerTask.Domain.Entities;
using ManagerTask.Domain.Repositories;
using ManagerTask.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ManagerTask.Infrastructure.Repositories
{
    public class ComentarioRepository : IRepository<Comentario>
    {
        private readonly AppDbContext _context;
        public ComentarioRepository(AppDbContext context)
            => _context = context;

        public async Task AddAsync(Comentario entity)
        {
            await _context.Comentarios.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comentario entity)
        {
            _context.Comentarios.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Comentario> GetByIdAsync(int id)
            => await _context.Comentarios.FirstOrDefaultAsync(x => x.Id == id) ?? new Comentario();


        public async Task<IEnumerable<Comentario>> GetAllAsync()
            => await _context.Comentarios.ToListAsync();


        public async Task RemoveAsync(Comentario entity)
        {
            _context.Comentarios.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
