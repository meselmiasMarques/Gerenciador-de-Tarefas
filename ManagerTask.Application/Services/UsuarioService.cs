using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerTask.Domain.Repositories;
using ManagerTask.Domain.Entities;

namespace ManagerTask.Application.Services
{
    public class UsuarioService
    {
        private readonly IRepository<Usuario> _repository;

        public UsuarioService(IRepository<Usuario> repository)
            => _repository = repository;

        public async Task AddAsync(Usuario usuario)
        {
            try
            {
                await _repository.AddAsync(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Usuario> UpdateAsync(Usuario usuario)
        {
            try
            {
                await _repository.UpdateAsync(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return usuario;
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var usuario = await _repository.GetByIdAsync(id);
                if (usuario != null)
                {
                    await _repository.RemoveAsync(usuario);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


        public async Task<Usuario> GetByIdAsync(int id)
        {
            try
            {
                var usuario = await _repository.GetByIdAsync(id);
                if (usuario != null)
                {
                    return usuario;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }


        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            try
            {
              var usuarios =  await _repository.GetAllAsync();

              if (usuarios != null)
              {
                  return usuarios;
              }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return new List<Usuario>();
        }

    }
}
