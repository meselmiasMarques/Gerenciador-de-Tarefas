//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Mail;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MyTask.Models;

//namespace Web.ManagerTask.Controllers
//{
//    public class TarefaController : Controller
//    {
//        private readonly DbContextImpacta _context;
//        private readonly IConfiguration _configuration;


//        public TarefaController(DbContextImpacta context, IConfiguration configuration)
//        {
//            _context = context;
//            _configuration = configuration;
//        }

//        public async Task<IActionResult> Index()
//        {

//            try
//            {
//                ViewData["ProjetoId"] = new SelectList(_context.Projetos, "Id", "Nome");
//                ViewData["UsuarioResponsavelId"] = new SelectList(_context.Usuarios, "Id", "Nome");

//                var tarefas = _context.Tarefas
//                        .Include(t => t.UsuarioResponsavel);

//                return View(await tarefas.ToListAsync());

//            }
//            catch (Exception)
//            {
//                ViewBag.Erro = "Erro ao Carregar Dados!";

//                return View();
//            }
//        }


//        public async Task<IActionResult> Detalhar(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var tarefa = await _context.Tarefas
//                .Include(t => t.UsuarioResponsavel)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (tarefa == null)
//            {
//                return NotFound();
//            }

//            return PartialView("_DetalharTarefa", tarefa);
//        }


//        public IActionResult Create(int projetoId)
//        {
//            ViewData["ProjetoId"] = projetoId;
//            ViewData["UsuarioResponsavelId"] = new SelectList(_context.Usuarios, "Id", "Nome");

//            return View();
//        }


//        [HttpPost]
//        public async Task<IActionResult> Create(Tarefa tarefa)
//        {
//            if (ModelState.IsValid)
//            {
//                tarefa.lActive = 1;
//                _context.Add(tarefa);
//                await _context.SaveChangesAsync();

//                Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == tarefa.UsuarioResponsavelId);
                
//                //registra cada task criada no historico de atividades
//                RegistrarHistorico(tarefa);
//                EnviarEmail(usuario, tarefa.Id, tarefa.Titulo);

//                return RedirectToAction("listarTarefasPorProjeto", "Projeto", new { id = tarefa.ProjetoId }, ViewBag.Sucesso);

//            }
//            ViewBag.Erro = "Erro ao cadastrar Tarefa!";

//            ViewData["UsuarioResponsavelId"] = new SelectList(_context.Usuarios, "Id", "Id", tarefa.UsuarioResponsavelId);
//            return View(tarefa);

//        }


//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var tarefa = await _context.Tarefas.FindAsync(id);
//            if (tarefa == null)
//            {
//                return NotFound();
//            }
//            ViewData["UsuarioResponsavelId"] = new SelectList(_context.Usuarios, "Id", "Id", tarefa.UsuarioResponsavelId);
//            return View(tarefa);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Edit(int id, Tarefa tarefa)
//        {
//            if (id != tarefa.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(tarefa);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!TarefaExists(tarefa.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction("Index");
//            }
//            ViewData["UsuarioResponsavelId"] = new SelectList(_context.Usuarios, "Id", "Id", tarefa.UsuarioResponsavelId);
//            return View(tarefa);
//        }

//        public IActionResult FinalizarTarefa(int id)
//        {

//            var tarefa = _context.Tarefas.FirstOrDefault(x => x.Id == id);
//            tarefa.Status = 0; //FLAG DE FINALIZAÇÃO

//            _context.Tarefas.Update(tarefa);
//            _context.SaveChanges();


//            return RedirectToAction("listarTarefasPorProjeto", "Projeto", new { id = tarefa.ProjetoId });
//        }

//        public IActionResult ReabrirTarefa(int id)
//        {

//            var tarefa = _context.Tarefas.FirstOrDefault(x => x.Id == id);
//            tarefa.Status = 1; //FLAG DE ABERTURA

//            _context.Tarefas.Update(tarefa);
//            _context.SaveChanges();



//            return RedirectToAction("listarTarefasPorProjeto", "Projeto", new { id = tarefa.ProjetoId });
//        }


//        [HttpPost]
//        public async Task<IActionResult> Excluir(int id)
//        {
//            try
//            {
//                var tarefa = await _context.Tarefas.FirstOrDefaultAsync(x => x.Id == id);

              
//                if (tarefa != null)
//                {
//                    tarefa.lActive = 0; //INATIVAÇÃO LÓGICA
//                    _context.Tarefas.Update(tarefa);
//                }

//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            catch (Exception ex)
//            {
//                ViewBag.Erro = "Erro ao Excluir tarefa, " + ex.Message;
//                return View();
//            }
//        }

//        private bool TarefaExists(int id)
//        {
//            return _context.Tarefas.Any(e => e.Id == id);
//        }

//        public void EnviarEmail(Usuario usuario, int tarefaId, string tarefaTitulo)
//        {
//            var smtpServer = _configuration["EmailSettings:SmtpServer"];
//            var port = int.Parse(_configuration["EmailSettings:Port"]);
//            var userName = _configuration["EmailSettings:UserName"];
//            var password = _configuration["EmailSettings:Password"];
//            var enableSsl = bool.Parse(_configuration["EmailSettings:EnableSsl"]);

//            var destinatario = usuario.Email;
//            var assunto = "Manager Task -  Nova Tarefa";
//            var corpo = @"
//                                <!DOCTYPE html>
//                                <html lang=""pt"">
//                                <head>
//                                    <meta charset=""UTF-8"">
//                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
//                                    <title>Email Template</title>
//                                    <style>
//                                       body {
//                                              font-family: Arial, sans-serif;
//                                              padding: 10px;
//                                            }
//                                            .container {
//                                              max-width: 400px;
//                                              background-color: #d3dfc3;
//                                              padding: 20px;
//                                              border-radius: 10px;
//                                              box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
//                                            }
//                                            .titulo {
//                                              font-size: 18px;
//                                              font-weight: bold;
//                                            }
//                                            .conteudo {
//                                              font-size: 16px;
//                                              border-bottom: absoluteove;
//                                              border: auto-flow;
//                                            }
//                                            .corpo {
//                                              font-size: 18px;
//                                              margin-bottom: 20px;
//                                            }


//                                    </style>
//                                </head>
//                                <body>
//                                    <div class=""container"">" +
//                                @"<p class=""corpo"">Olá, " + usuario.Nome + "</p>" +
//                                   @"<p class=""conteudo"">A tarefa <span class=""titulo"">" + tarefaId + " - " + tarefaTitulo + "</span> foi atribuída a você!</p></div></body></html>";


//            using (var mensagem = new MailMessage(userName, destinatario))
//            using (var clienteSmtp = new SmtpClient(smtpServer, port))
//            {
//                clienteSmtp.Credentials = new NetworkCredential(userName, password);
//                clienteSmtp.EnableSsl = enableSsl;

//                mensagem.Subject = assunto;
//                mensagem.Body = corpo;
//                mensagem.IsBodyHtml = true; // Define o corpo como HTML


//                try
//                {
//                    clienteSmtp.Send(mensagem);
//                    ViewBag.Mensagem = "E-mail enviado com sucesso!";
//                }
//                catch (SmtpException ex)
//                {
//                    ViewBag.Mensagem = "Erro ao enviar o e-mail: " + ex.Message;
//                }
//            }

//        }


//        public void RegistrarHistorico(Tarefa tarefa)
//        {

//            HistoricoAtividade historio = new();
//            historio.TarefaId = tarefa.Id;
//            historio.UsuarioId = tarefa.UsuarioResponsavelId;
//            historio.TipoAtividade = tarefa.Titulo;
//            historio.DataHoraAtividade = tarefa.DataCriacao;

//            var historico = _context.HistoricoAtividades.Add(historio);
//            _context.SaveChanges();
//        }
//    }
//}


  