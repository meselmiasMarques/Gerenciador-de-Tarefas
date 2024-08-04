using ManagerTask.Domain.Repositories;
using ManagerTask.Infrastructure.Data;
using ManagerTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagerTask.Infrastructure.Repositories
{
    public class HistoricoAtividadeRepository : IRepository<HistoricoAtividade>
    {
        private readonly AppDbContext _context;
        public HistoricoAtividadeRepository(AppDbContext context)
            => _context = context;

        public async Task AddAsync(HistoricoAtividade entity)
        {
            await _context.HistoricoAtividades.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(HistoricoAtividade entity)
        {
            _context.HistoricoAtividades.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<HistoricoAtividade> GetByIdAsync(int id)
            => await _context.HistoricoAtividades.FirstOrDefaultAsync(x => x.Id == id) ?? new HistoricoAtividade();


        public async Task<IEnumerable<HistoricoAtividade>> GetAllAsync()
            => await _context.HistoricoAtividades.ToListAsync();


        public async Task RemoveAsync(HistoricoAtividade entity)
        {
            _context.HistoricoAtividades.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
