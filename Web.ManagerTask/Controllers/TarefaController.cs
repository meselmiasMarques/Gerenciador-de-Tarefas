using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTask.Models;

namespace Web.ManagerTask.Controllers
{
    public class TarefaController : Controller
    {
        private readonly DbContextImpacta _context;

        public TarefaController(DbContextImpacta context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            try
            {
                ViewData["ProjetoId"] = new SelectList(_context.Projetos, "Id", "Nome");
                ViewData["UsuarioResponsavelId"] = new SelectList(_context.Usuarios, "Id", "Nome");

                var tarefas =  _context.Tarefas
                        .Include(t => t.UsuarioResponsavel);
                       
                return View(await tarefas.ToListAsync());

            }
            catch (Exception)
            {
                ViewBag.Erro = "Erro ao Carregar Dados!";

                return View(); 
            }
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .Include(t => t.UsuarioResponsavel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }


        public IActionResult Create()
        {
            ViewData["UsuarioResponsavelId"] = new SelectList(_context.Usuarios, "Id", "Nome");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioResponsavelId"] = new SelectList(_context.Usuarios, "Id", "Id", tarefa.UsuarioResponsavelId);
            return View(tarefa);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            ViewData["UsuarioResponsavelId"] = new SelectList(_context.Usuarios, "Id", "Id", tarefa.UsuarioResponsavelId);
            return View(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,  Tarefa tarefa)
        {
            if (id != tarefa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["UsuarioResponsavelId"] = new SelectList(_context.Usuarios, "Id", "Id", tarefa.UsuarioResponsavelId);
            return View(tarefa);
        }

        public IActionResult TodoDone(int id) {
    
            var tarefa = _context.Tarefas.FirstOrDefault(x => x.Id == id);
            tarefa.Status = true;

            _context.Tarefas.Update(tarefa);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        public IActionResult Todoopen(int id)
        {

            var tarefa = _context.Tarefas.FirstOrDefault(x => x.Id == id);
                tarefa.Status = false;

            _context.Tarefas.Update(tarefa);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefas
                .Include(t => t.UsuarioResponsavel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

 
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa != null)
            {
                _context.Tarefas.Remove(tarefa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefas.Any(e => e.Id == id);
        }
    }
}
