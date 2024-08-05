using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerTask.Domain.Entities;

namespace ManagerTask.Domain.Repositories
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        Task<List<Tarefa>> ListTaskUser();
    }
}
