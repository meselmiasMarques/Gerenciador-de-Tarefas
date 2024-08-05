using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerTask.Domain.Entities;

namespace ManagerTask.Domain.Services
{
    public interface IProjetoService : IService<Projeto>
    {
        Task<List<Tarefa>> ListTaskByProject(int id);

        Task<IEnumerable<Projeto>> ListProjectTask();

    }
}
