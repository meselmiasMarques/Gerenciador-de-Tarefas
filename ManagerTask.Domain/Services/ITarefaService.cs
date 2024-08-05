using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerTask.Domain.Entities;

namespace ManagerTask.Domain.Services
{
    public interface ITarefaService : IService<Tarefa>
    {
        Task<Tarefa> FinishTask(int id);
        Task<Tarefa> ReOpenTask(int id);
        Task<List<Tarefa>> ListTaskUser();
    }
}
