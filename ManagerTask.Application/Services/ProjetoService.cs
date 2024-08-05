using ManagerTask.Domain.Entities;
using ManagerTask.Domain.Repositories;
using ManagerTask.Domain.Services;

namespace ManagerTask.Application.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly IProjetoRepository _repository;
        public ProjetoService(IProjetoRepository repository)
            => _repository = repository;
        public async Task AddAsync(Projeto? entities)
        {
            try
            {
                if (entities != null)
                {
                    entities.Tarefas = new List<Tarefa>();
                    entities.Status = true;
                    entities.DataCriacaoProjeto = DateTime.Now;
                    await _repository.AddAsync(entities);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task UpdateAsync(Projeto? entities)
        {
            try
            {
                if (entities != null)
                {
                    await _repository.UpdateAsync(entities);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task DeleteAsync(Projeto? entities)
        {
            try
            {
                if (entities != null)
                {
                    await _repository.RemoveAsync(entities);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<Projeto>> GetAllAsync()
        {
            try
            {
                var projetos = await _repository.GetAllAsync();
                return projetos;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Projeto> GetByIdAsync(int id)
        {
            try
            {
                if (id > 0)
                {
                    var projeto = await _repository.GetByIdAsync(id);
                    return projeto;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

        public async Task<List<Tarefa>> ListTaskByProject(int id)
        {

            try
            {
                if (id > 0)
                {
                    var tarefas = await _repository.ListTaskByProject(id);
                    return tarefas;
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

        public Task<IEnumerable<Projeto>> ListProjectTask()
        {
            try
            {
              return _repository.ListProjectTask();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
