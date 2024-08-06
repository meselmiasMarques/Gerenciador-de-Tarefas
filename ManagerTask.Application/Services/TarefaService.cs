
using System.Diagnostics.CodeAnalysis;
using ManagerTask.Domain.Repositories;
using ManagerTask.Domain.Entities;
using ManagerTask.Domain.Services;

namespace ManagerTask.Application.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repository;
        public TarefaService(ITarefaRepository repository)
            => _repository = repository;

        public async Task AddAsync(Tarefa? entity)
        {

            try
            {
                if (entity != null)
                {
                    entity.DataCriacao = DateTime.Now;
                    entity.Status = 1;
                    entity.LActive = 1;
                    await _repository.AddAsync(entity);
                }
            }
            catch
            {
            }
        }

        public async Task UpdateAsync(Tarefa? entity)
        {
            try
            {
                if (entity != null)
                {
                    _repository.UpdateAsync(entity);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task DeleteAsync(Tarefa? entity)
        {
            if (entity != null)
            {
                await _repository.RemoveAsync(entity);
            }
        }

        public async Task<IEnumerable<Tarefa>> GetAllAsync()
        {
            try
            {
                var tarefas = await _repository.GetAllAsync();
                return tarefas;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Tarefa> GetByIdAsync(int id)
        {
            try
            {
                if (id > 0)
                {
                    var tarefa = await _repository.GetByIdAsync(id);
                    return tarefa;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return null;
        }

        public async Task<Tarefa> FinishTask(int id)
        {

            var tarefa = await _repository.GetByIdAsync(id);
            tarefa.Status = 0; //flag de finalização
            await _repository.UpdateAsync(tarefa);
            return tarefa;
        }

        public async Task<Tarefa> ReOpenTask(int id)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            tarefa.Status = 1; //flag de finalização
            await _repository.UpdateAsync(tarefa);
            return tarefa;
        }

        public async Task<List<Tarefa>> ListTaskUser()
        {
            try
            {
                return await _repository.ListTaskUser();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
