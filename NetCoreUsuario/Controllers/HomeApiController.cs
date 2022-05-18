using Microsoft.AspNetCore.Mvc;
using Model.Entidades;
using NetCoreUsuario.Models;
using Services;
using System.Collections.Generic;
using System.IO;

namespace NetCoreUsuario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeApiController : ControllerBase
    {
        public IUsuarioService _usuarioService;
        public HomeApiController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public ApiResponseVM Index()
        {
            try
            {
                IEnumerable<Usuario> usuarios = _usuarioService.GetAllUsers();
                return new ApiResponseVM() { success = true, data = usuarios };
            }
            catch
            {
                return new ApiResponseVM() { success = false, message = "Error interno del servidor al obtener usuarios, contacte al administrador" };
            }
        }

        [HttpPost]
        public ApiResponseVM InsertarUsuario(UsuarioVM usuarioVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ApiResponseVM() { success = false, message = "Modelo Inválido" };
                }

                string rutaImagen = "wwwroot\\uploads\\" + usuarioVM.Fotografia.FileName;

                using (FileStream fs = new FileStream(rutaImagen, FileMode.Create))
                {
                    usuarioVM.Fotografia.CopyTo(fs);
                }

                Usuario usuario = new Usuario()
                {
                    NombreCompleto = usuarioVM.NombreCompleto,
                    Direccion = usuarioVM.Direccion,
                    PerfilGeneral = usuarioVM.PerfilGeneral,
                    Fotografia = "uploads/" + usuarioVM.Fotografia.FileName
                };

                _usuarioService.Create(usuario);
                return new ApiResponseVM() { success = true, message = "Usuario registrado con éxito" };
            }
            catch
            {
                return new ApiResponseVM() { success = false, message = "Error interno del servidor al registrar usuario, contacte al administrador" };
            }
        }

        public ApiResponseVM Eliminar(int id)
        {
            try
            {
                _usuarioService.Eliminar(id);
                return new ApiResponseVM() { success = true, message = "Usuario eliminado con éxito" };
            }
            catch
            {
                return new ApiResponseVM() { success = false, message = "Error interno del servidor al eliminar usuario, contacte al administrador" };
            }
        }

        public ApiResponseVM Actualizar(int id)
        {
            try
            {
                Usuario usuario = _usuarioService.GetUserById(id);
                return new ApiResponseVM() { success = true, message = "Usuario actualizado con éxito", data = usuario };
            }
            catch
            {
                return new ApiResponseVM() { success = false, message = "Error interno del servidor al obtener usuario a actualizar, contacte al administrador" };
            }
        }

        [HttpPost]
        public ApiResponseVM ActualizarUsuario(Usuario usuario)
        {
            try
            {
                _usuarioService.ActualizarUsuario(usuario);
                return new ApiResponseVM() { success = true, message = "Usuario actualizado con éxito" };
            }
            catch
            {
                return new ApiResponseVM() { success = false, message = "Error interno del servidor al actualizar usuario, contacte al administrador" };
            }
        }
    }
}
