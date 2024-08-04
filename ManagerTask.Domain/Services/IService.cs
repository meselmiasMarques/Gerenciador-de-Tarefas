using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerTask.Domain.Services
{
    public interface IService<T> where T : class
    { 
        Task AddAsync(T entities);
        Task UpdateAsync(T entities);
        Task DeleteAsync(T entities);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
    }
}
