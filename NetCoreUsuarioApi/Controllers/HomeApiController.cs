using Microsoft.AspNetCore.Mvc;
using Model.Entidades;
using NetCoreUsuario.Models;
using Services;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreUsuarioApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeApiController : ControllerBase
    {
        public IUsuarioService _usuarioService;
        public IPerfilesService _perfilesService;
        public HomeApiController(IUsuarioService usuarioService, IPerfilesService perfilesService)
        {
            _usuarioService = usuarioService;
            _perfilesService = perfilesService;
        }

        [HttpGet]
        [Route("ObtenerPerfiles")]
        public ApiResponseVM ObtenerPerfiles()
        {
            try
            {
                List<Perfiles> perfiles = _perfilesService.GetAllPerfiles().ToList();
                return new ApiResponseVM() { success = true, data = perfiles };
            }
            catch
            {
                return new ApiResponseVM() { success = false, message = "Error interno del servidor al obtener usuarios, contacte al administrador" };
            }
        }

        [Route("ObtenerUsuarios")]
        [HttpGet]
        public ApiResponseVM Index()
        {
            try
            {
                List<Perfiles> perfiles = _perfilesService.GetAllPerfiles().ToList();
                IEnumerable<Models.UsuarioVM> usuarios = (from users in _usuarioService.GetAllUsers()
                                                          join perfil in perfiles on users.PerfilGeneral equals perfil.Id
                                                          select new Models.UsuarioVM()
                                                          {
                                                              Id = users.Id,
                                                              NombreCompleto = users.NombreCompleto,
                                                              Direccion = users.Direccion,
                                                              PerfilGeneral = users.PerfilGeneral,
                                                              PerfilDescripcion = perfil.Perfil,
                                                              FechaCreacion = users.FechaCreacion.ToString("dd/MM/yyyy HH:mm tt")
                                                          }).ToList();
                return new ApiResponseVM() { success = true, data = usuarios };
            }
            catch
            {
                return new ApiResponseVM() { success = false, message = "Error interno del servidor al obtener usuarios, contacte al administrador" };
            }
        }

        [Route("InsertarUsuario")]
        [HttpPost]
        public ApiResponseVM InsertarUsuario(UsuarioVM usuarioVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ApiResponseVM() { success = false, message = "Modelo Inválido" };
                }

                Usuario usuario = new Usuario()
                {
                    NombreCompleto = usuarioVM.NombreCompleto,
                    Direccion = usuarioVM.Direccion,
                    PerfilGeneral = usuarioVM.PerfilGeneral
                };

                _usuarioService.Create(usuario);
                return new ApiResponseVM() { success = true, message = "Usuario registrado con éxito" };
            }
            catch
            {
                return new ApiResponseVM() { success = false, message = "Error interno del servidor al registrar usuario, contacte al administrador" };
            }
        }

        [HttpDelete]
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

        [HttpGet]
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
