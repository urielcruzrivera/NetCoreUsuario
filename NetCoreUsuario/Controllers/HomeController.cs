using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Model.Entidades;
using NetCoreUsuario.Models;
using Services;

namespace NetCoreUsuario.Controllers
{
    public class HomeController : Controller
    {
        public IUsuarioService _usuarioService;
        public HomeController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            IEnumerable<Usuario> usuarios = _usuarioService.GetAllUsers();
            return View(usuarios);
        }

        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InsertarUsuario(UsuarioVM usuarioVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Nuevo", usuarioVM);
            }

            Usuario usuario = new Usuario()
            {
                NombreCompleto = usuarioVM.NombreCompleto,
                Direccion = usuarioVM.Direccion,
                PerfilGeneral = usuarioVM.PerfilGeneral
            };

            _usuarioService.Create(usuario);
            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            _usuarioService.Eliminar(id);
            return RedirectToAction("Index");
        }

        public IActionResult Actualizar(int id)
        {
            Usuario usuario = _usuarioService.GetUserById(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult ActualizarUsuario(Usuario usuario)
        {
            _usuarioService.ActualizarUsuario(usuario);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
