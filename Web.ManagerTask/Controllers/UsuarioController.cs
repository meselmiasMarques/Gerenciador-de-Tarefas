using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTask.Models;

namespace Web.ManagerTask.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DbContextImpacta _context;

        public UsuarioController(DbContextImpacta context)
        {
            _context = context;
        }

        public async Task<IActionResult> lista()
            => View(await _context.Usuarios.ToListAsync());
        
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Erro ao cadastrar usuário";
                return View(usuario);
            }
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("lista");

        }

    }
}
