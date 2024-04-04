using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTask.Models;

namespace Web.ManagerTask.Controllers
{
    public class HistoricoAtividadesController : Controller
    {
        private readonly DbContextImpacta _context;

        public HistoricoAtividadesController(DbContextImpacta context)
            => _context = context;
        public async Task<IActionResult> Lista()
        {
           var historico = await _context.HistoricoAtividades.Include(x => x.Usuario).Include(x => x.Tarefa).ToListAsync();

            return View(historico);
        }
    }
}
