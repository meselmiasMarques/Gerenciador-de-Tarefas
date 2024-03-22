using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTask.Models;

namespace Web.ManagerTask.Controllers
{
    public class ProjetoController : Controller
    {
        private readonly DbContextImpacta _context;

        public ProjetoController(DbContextImpacta context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
        
            var projetos = await _context.Projetos
                    .Include(t => t.Tarefas).ToListAsync();
            return View(projetos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Projeto projeto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Erro ao criar projeto!";
                return View(projeto);
            }
            await _context.Projetos.AddAsync(projeto);
            await _context.SaveChangesAsync();


            return RedirectToAction("Lista");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var projeto = await _context.Projetos.FirstOrDefaultAsync(x => x.Id == id);

            if (projeto == null)
                ViewBag.Erro = "Erro ao Recuperar Projeto";
           

            return View(projeto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Projeto model)
        {
            _context.Projetos.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Lista");
        }

        [HttpPost]
        public IActionResult Excluir(int id)
        {
            var tarefas = _context.Tarefas.Where(x => x.ProjetoId == id).ToList();
            foreach (var tarefa in tarefas)
            {
                tarefa.Status = 0;
            }
            _context.SaveChanges();

            var projeto = _context.Projetos.FirstOrDefault(p => p.Id == id);

          if (projeto == null)
            {
                ViewBag.Erro = "Erro ao excluir";
                return View();
            }
            _context.Projetos.Remove(projeto);
            _context.SaveChanges();
            ViewBag.Sucesso = "Projeto Excluído com sucesso !";

            return RedirectToAction("Lista");
        }


        [HttpGet]
        public async Task<IActionResult> listarTarefasPorProjeto(int id)
        {
            if (id == null)
            {
                ViewBag.Erro = "Não foi possível recuperar as Tafefas !";
                return View();
            }

            try
            {
                var tarefas = await _context.Tarefas
                    .Where(p => p.ProjetoId == id)
                    .ToListAsync();

                if (tarefas.Count != 0)
                {
                    ViewBag.ProjetoId = tarefas.FirstOrDefault(x => x.ProjetoId == id).ProjetoId;
                }
                ViewBag.ProjetoId = id;



                return View(tarefas);
            }
            catch (Exception e)
            {
                ViewBag.Erro = e.ToString();
                return View();
                throw;
            }
        }

    }
}
