using ManagerTask.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManagerTask.Domain.Entities;

using Usuario = ManagerTask.Domain.Entities.Usuario;

namespace Web.ManagerTask.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

       
        public async Task<IActionResult> lista()
        {
            //var usuarios = _context.Usuarios.Include(x => x.Tarefas).ToList();
            var usuarios = await _service.GetAllAsync();
            
            return View(usuarios);
        }

          

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(global::ManagerTask.Domain.Entities.Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Erro ao cadastrar usuário";
                return RedirectToAction("lista");
            }
            await _service.AddAsync(usuario);
            return RedirectToAction("lista");

        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _service.GetByIdAsync(id);

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _service.UpdateAsync(usuario);
                }
                catch (DbUpdateConcurrencyException)
                {
                    ViewBag.Erro = "Erro ao Alterar Usuário";
                }
                return RedirectToAction("lista");
            }
            return View(usuario); ;
        }


        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {

                //if (usuario.Tarefas.Count >= 1)
                //{
                //    ViewBag.Erro = "Este usuário possui tarefas associadas, por favor, exclua ou conclua as tarefas associadas";

                //    return View();
                //}

                await _service.DeleteAsync(id);
                return RedirectToAction("lista");
                
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Erro ao Excluir usuario " + ex.Message;
                return View();
            }

        }

    }
}
