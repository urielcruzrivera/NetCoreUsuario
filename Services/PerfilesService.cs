using Model;
using Model.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public interface IPerfilesService
    {
        IEnumerable<Perfiles> GetAllPerfiles();
    }

    public class PerfilesService : IPerfilesService
    {
        private UsuarioDbContext _context;
        public PerfilesService(UsuarioDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Perfiles> GetAllPerfiles()
        {
            IEnumerable<Perfiles> perfiles = new List<Perfiles>();
            try
            {
                perfiles = _context.Perfiles.OrderBy(x => x.Id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return perfiles;
        }

    }
}
