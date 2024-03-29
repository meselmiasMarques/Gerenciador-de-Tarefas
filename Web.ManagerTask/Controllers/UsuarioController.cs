﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [HttpGet]
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
                return RedirectToAction("lista");
            }
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("lista");

        }

        //[HttpGet]
        //public IActionResult Cadastrar()
        //{
        //    return PartialView("_Cadastrar");

        //}

        //[HttpPost]
        //public async Task<IActionResult> Cadastrar(Usuario usuario)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Erro = "Erro ao cadastrar usuário";
        //       return RedirectToAction("lista");
        //    }
        //    _context.Add(usuario);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("lista");

        //}

        //[HttpGet]
        //public async Task<IActionResult> Editar(int id)
        //{
        //    var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

        //    return PartialView("_Editar", usuario);
        //}

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);

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
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ViewBag.Erro = "Erro ao Alterar Usuário";
                }
                return RedirectToAction("lista");
            }
            return View(usuario); ;
        }


        [HttpPost, ActionName("Excluir")]
        public async Task<IActionResult> Excluir(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("lista");
        }

    }
}
