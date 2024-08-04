
using ManagerTask.Domain.Repositories;
using ManagerTask.Domain.Entities;
using ManagerTask.Domain.Services;

namespace ManagerTask.Application.Services
{
    public class TarefaService : IService<Tarefa>
    {
        private readonly IRepository<Tarefa> _repository;
        public TarefaService(IRepository<Tarefa> repository)
            => _repository = repository;

        public async Task AddAsync(Tarefa entity)
        {

            if (entity != null)
            {
                entity.DataCriacao = DateTime.Now;
                entity.Status = 0;
                await _repository.AddAsync(entity);
            }
        }

        public async Task UpdateAsync(Tarefa entity)
            => await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(Tarefa entity)
            => await _repository.RemoveAsync(entity);

        public async Task<IEnumerable<Tarefa>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Tarefa> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);
    }
}
