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
    public class ProjetoRepository : IRepository<Projeto>
    {
        private readonly AppDbContext _context;
        public ProjetoRepository(AppDbContext context)
            => _context = context;

        public async Task AddAsync(Projeto entity)
        {
            await _context.Projetos.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Projeto entity)
        {
            _context.Projetos.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Projeto> GetByIdAsync(int id)
            => await _context.Projetos.FirstOrDefaultAsync(x => x.Id == id) ?? new Projeto();


        public async Task<IEnumerable<Projeto>> GetAllAsync()
            => await _context.Projetos.ToListAsync();


        public async Task RemoveAsync(Projeto entity)
        {
            _context.Projetos.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
