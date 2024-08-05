using System.Net;
using System.Net.Mail;
using ManagerTask.Application.Services;
using ManagerTask.Domain.Entities;
using ManagerTask.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTask.Models;
using HistoricoAtividade = ManagerTask.Domain.Entities.HistoricoAtividade;
using Usuario = ManagerTask.Domain.Entities.Usuario;


namespace Web.ManagerTask.Controllers
{
    public class TarefaController : Controller
    {
        private readonly ITarefaService _tarefaService;
        private readonly IProjetoService _projetoService;
        private readonly UsuarioService _usuarioService;

        private readonly IConfiguration _configuration;


        public TarefaController(
            ITarefaService tarefaService,
            IProjetoService projetoService,
            UsuarioService usuarioService,
            IConfiguration configuration
            )
        {
            _tarefaService = tarefaService;
            _projetoService = projetoService;
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {

            try
            {
                ViewData["ProjetoId"] = new SelectList(await _projetoService.GetAllAsync(), "Id", "Nome");
                ViewData["UsuarioResponsavelId"] = new SelectList(await _usuarioService.GetAllAsync(), "Id", "Nome");

                IEnumerable<TarefaViewModel> tarefas = (IEnumerable<TarefaViewModel>)await _tarefaService.ListTaskUser();

                return View(tarefas);

            }
            catch (Exception)
            {
                ViewBag.Erro = "Erro ao Carregar Dados!";

                return View();
            }
        }


        public async Task<IActionResult> Detalhar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            IEnumerable<Tarefa> tarefas = (IEnumerable<Tarefa>)await _tarefaService.ListTaskUser();

            var tarefa = tarefas.FirstOrDefault(x => x.Id == id);


            var tarefaviewmodel = new TarefaViewModel
            {
                Status = tarefa.Status,
                Descricao = tarefa.Descricao,
                Titulo = tarefa.Titulo
            };



            if (tarefa == null)
            {
                return NotFound();
            }

            return PartialView("_DetalharTarefa", tarefaviewmodel);
        }


        public async Task<IActionResult> Create(int projetoId)
        {
            ViewData["ProjetoId"] = projetoId;
            ViewData["UsuarioResponsavelId"] = new SelectList(await _usuarioService.GetAllAsync(), "Id", "Nome");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(TarefaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tarefa = new Tarefa
                {
                    Titulo = model.Titulo,
                    Status = model.Status,
                    Descricao = model.Descricao,
                    ProjetoId = model.ProjetoId,
                    UsuarioResponsavelId = model.UsuarioResponsavelId
                };

                await _tarefaService.AddAsync(tarefa);

                Usuario? usuario = await _usuarioService.GetByIdAsync(tarefa.UsuarioResponsavelId);


                //registra cada task criada no historico de atividades
                RegistrarHistorico(tarefa);
                EnviarEmail(usuario, tarefa.Id, tarefa.Titulo);

                return RedirectToAction("listarTarefasPorProjeto", "Projeto", new { id = tarefa.ProjetoId }, ViewBag.Sucesso);

            }
            ViewBag.Erro = "Erro ao cadastrar Tarefa!";

            ViewData["UsuarioResponsavelId"] = new SelectList(await _usuarioService.GetAllAsync(), "Id", "Nome");
            return View();

        }


        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _tarefaService.GetByIdAsync(id);

            var tarefa = new TarefaViewModel()
            {
                Titulo = model.Titulo,
                Descricao = model.Descricao,
                Status = model.Status,
                ProjetoId = model.ProjetoId,
                UsuarioResponsavelId = model.UsuarioResponsavelId,
                lActive = model.LActive
            };

            if (tarefa == null)
            {
                return NotFound();
            }
            ViewData["UsuarioResponsavelId"] = new SelectList(await _usuarioService.GetAllAsync(), "Id", "Nome");
            return View(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TarefaViewModel model)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    var tarefa = new Tarefa()
                    {
                        Titulo = model.Titulo,
                        Descricao = model.Descricao,
                        Status = model.Status,
                        UsuarioResponsavelId = model.UsuarioResponsavelId,
                        LActive = model.lActive,
                        ProjetoId = model.ProjetoId
                    };

                    await _tarefaService.UpdateAsync(tarefa);
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction("Index");
            }
            ViewData["UsuarioResponsavelId"] = new SelectList(await _usuarioService.GetAllAsync(), "Id", "Nome");
            ;
            return View();
        }

        public async Task<IActionResult> FinalizarTarefa(int id)
        {

            var tarefa = await _tarefaService.FinishTask(id);

            return RedirectToAction("listarTarefasPorProjeto", "Projeto", new { id = tarefa.ProjetoId });
        }

        public async Task<IActionResult> ReabrirTarefa(int id)
        {

            var tarefa = await _tarefaService.ReOpenTask(id);


            return RedirectToAction("listarTarefasPorProjeto", "Projeto", new { id = tarefa.ProjetoId });
        }


        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                var tarefa = await _tarefaService.GetByIdAsync(id);


                if (tarefa != null)
                {
                    _tarefaService.DeleteAsync(tarefa);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Erro = "Erro ao Excluir tarefa, " + ex.Message;
                return RedirectToAction(nameof(Index));

            }
        }



        public void EnviarEmail(Usuario usuario, int tarefaId, string tarefaTitulo)
        {
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var port = int.Parse(_configuration["EmailSettings:Port"]);
            var userName = _configuration["EmailSettings:UserName"];
            var password = _configuration["EmailSettings:Password"];
            var enableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]);

            var destinatario = usuario.Email;
            var assunto = "Manager Task -  Nova Tarefa";
            var corpo = @"
                                <!DOCTYPE html>
                                <html lang=""pt"">
                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <title>Email Template</title>
                                    <style>
                                       body {
                                              font-family: Arial, sans-serif;
                                              padding: 10px;
                                            }
                                            .container {
                                              max-width: 400px;
                                              background-color: #d3dfc3;
                                              padding: 20px;
                                              border-radius: 10px;
                                              box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                            }
                                            .titulo {
                                              font-size: 18px;
                                              font-weight: bold;
                                            }
                                            .conteudo {
                                              font-size: 16px;
                                              border-bottom: absoluteove;
                                              border: auto-flow;
                                            }
                                            .corpo {
                                              font-size: 18px;
                                              margin-bottom: 20px;
                                            }


                                    </style>
                                </head>
                                <body>
                                    <div class=""container"">" +
                                @"<p class=""corpo"">Olá, " + usuario.Nome + "</p>" +
                                   @"<p class=""conteudo"">A tarefa <span class=""titulo"">" + tarefaId + " - " + tarefaTitulo + "</span> foi atribuída a você!</p></div></body></html>";


            using (var mensagem = new MailMessage(userName, destinatario))
            using (var clienteSmtp = new SmtpClient(smtpServer, port))
            {
                clienteSmtp.Credentials = new NetworkCredential(userName, password);
                clienteSmtp.EnableSsl = enableSsl;

                mensagem.Subject = assunto;
                mensagem.Body = corpo;
                mensagem.IsBodyHtml = true; // Define o corpo como HTML


                try
                {
                    clienteSmtp.Send(mensagem);
                    ViewBag.Mensagem = "E-mail enviado com sucesso!";
                }
                catch (SmtpException ex)
                {
                    ViewBag.Mensagem = "Erro ao enviar o e-mail: " + ex.Message;
                }
            }

        }


        public void RegistrarHistorico(Tarefa tarefa)
        {

            //HistoricoAtividade historio = new();
            //historio.TarefaId = tarefa.Id;
            //historio.UsuarioId = tarefa.UsuarioResponsavelId;
            //historio.TipoAtividade = tarefa.Titulo;
            //historio.DataHoraAtividade = tarefa.DataCriacao;

            //var historico = _context.HistoricoAtividades.Add(historio);
            //_context.SaveChanges();
        }
    }
}


