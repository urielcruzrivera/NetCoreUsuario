using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Entidades;

namespace Services
{
    public interface IUsuarioService
    {
        bool Create(Usuario usuario);
        IEnumerable<Usuario> GetAllUsers();
        bool Eliminar(int id);
        Usuario GetUserById(int id);
        bool ActualizarUsuario(Usuario usuario);
    }

    public class UsuarioService : IUsuarioService
    {
        private UsuarioDbContext _context;
        public UsuarioService(UsuarioDbContext context)
        {
            _context = context;
        }

        public bool Create(Usuario usuario)
        {
            try
            {
                usuario.FechaCreacion = DateTime.Now;
                _context.Entry(usuario).State = EntityState.Added;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Usuario> GetAllUsers() 
        {
            IEnumerable<Usuario> usuario = new List<Usuario>();
            try
            {
                usuario = _context.Usuario.OrderByDescending(x => x.FechaCreacion).ToList();
            }
            catch (Exception ex)
            {
            }
            return usuario;
        }

        public bool Eliminar(int id)
        {
            try
            {
                Usuario usuarioEliminar = _context.Usuario.Where(x => x.Id == id).FirstOrDefault();
                _context.Usuario.Remove(usuarioEliminar);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Usuario GetUserById(int id)
        {
           return _context.Usuario.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool ActualizarUsuario(Usuario usuario) 
        {
            try
            {
                Usuario usuarioActualizar = _context.Usuario.Where(x => x.Id == usuario.Id).FirstOrDefault();
                usuarioActualizar.NombreCompleto = usuario.NombreCompleto;
                usuarioActualizar.PerfilGeneral = usuario.PerfilGeneral;
                usuarioActualizar.Direccion = usuario.Direccion;
                _context.Usuario.Update(usuarioActualizar);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
