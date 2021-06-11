using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Entidades;
using NetCoreUsuario.Models;
using Services;

namespace NetCoreUsuario.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

            string rutaImagen = "wwwroot\\uploads\\" + usuarioVM.Fotografia.FileName;

            using (FileStream fs = new FileStream(rutaImagen, FileMode.Create))
            {
                usuarioVM.Fotografia.CopyTo(fs);
            };

            Usuario usuario = new Usuario()
            {
                NombreCompleto = usuarioVM.NombreCompleto,
                Direccion = usuarioVM.Direccion,
                PerfilGeneral = usuarioVM.PerfilGeneral,
                Fotografia = "uploads/" + usuarioVM.Fotografia.FileName
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
