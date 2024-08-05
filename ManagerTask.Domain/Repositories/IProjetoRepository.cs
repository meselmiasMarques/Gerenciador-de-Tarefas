using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerTask.Domain.Entities;

namespace ManagerTask.Domain.Repositories
{
    public interface IProjetoRepository :IRepository<Projeto>
    {
        Task<List<Tarefa>> ListTaskByProject(int id);

        Task<IEnumerable<Projeto>> ListProjectTask();

    }
}
