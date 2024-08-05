using ManagerTask.Domain.Entities;
using ManagerTask.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTask.Models;

namespace Web.ManagerTask.Controllers
{
    public class ProjetoController : Controller
    {
        private readonly IProjetoService _service;

        public ProjetoController(IProjetoService service)
            => _service = service;


        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var projetos = await _service.ListProjectTask();

            return View(projetos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(ProjetoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Erro = "Erro ao criar projeto!";
                return View(model);
            }


            var projeto = new Projeto()
            {
                NomeProjeto = model.NomeProjeto,
                Status = model.Status,
                Tarefas = new List<Tarefa>(),
                DescricaoProjeto = model.DescricaoProjeto,
                DataCriacaoProjeto = DateTime.Now,
                UsuarioCriadorId = model.UsuarioCriadorId

            };


            await _service.AddAsync(projeto);

            ViewBag.Sucesso = "Projeto cadastrado com sucesso!";

            return View("Lista", await _service.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var projeto = await _service.GetByIdAsync(id);

            if (projeto == null)
                ViewBag.Erro = "Erro ao Recuperar Projeto";


            return View(projeto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProjetoViewModel model)
        {
            var projeto = new Projeto()
            {
                NomeProjeto = model.NomeProjeto,
                Status = model.Status,
                Tarefas = new List<Tarefa>(),
                DescricaoProjeto = model.DescricaoProjeto,
                DataCriacaoProjeto = DateTime.Now,
                UsuarioCriadorId = model.UsuarioCriadorId

            };

            await _service.UpdateAsync(projeto);
            return RedirectToAction("Lista");
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            var tarefas = await _service.ListTaskByProject(id);

            foreach (var tarefa in tarefas)
            {
                tarefa.Status = 0;
            }


            var projeto = await _service.GetByIdAsync(id);

            if (projeto == null)
            {
                ViewBag.Erro = "Erro ao excluir";
              
            }

            await _service.DeleteAsync(projeto);
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
                var tarefas = await _service.ListTaskByProject(id);

                if (tarefas.Count != 0)
                {
                    ViewBag.ProjetoId = tarefas.FirstOrDefault(x => x.ProjetoId == id)!.ProjetoId;
                    
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
