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
    public class TarefaRepository : ITarefaRepository

    {
        private readonly AppDbContext _context;

        public TarefaRepository(AppDbContext context)
            => _context = context;

        public async Task AddAsync(Tarefa entity)
        {
            await _context.Tarefas.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tarefa entity)
        {
            _context.Tarefas.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Tarefa> GetByIdAsync(int id)
            => await _context.Tarefas.FirstOrDefaultAsync(x => x.Id == id) ?? new Tarefa();


        public async Task<IEnumerable<Tarefa>> GetAllAsync()
            => await _context.Tarefas.ToListAsync();


        public async Task RemoveAsync(Tarefa entity)
        {
            _context.Tarefas.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<List<Tarefa>> ListTaskUser()
        {
            var tarefas = _context
                .Tarefas
                .AsNoTracking()
                .Include(x => x.UsuarioResponsavel)
                .ToListAsync();
            return tarefas;
        }

        public Task<List<Tarefa>> ListTaskByProject()
        {
            throw new NotImplementedException();
        }
    }
}
