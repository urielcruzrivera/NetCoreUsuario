using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetCoreUsuario.Models
{
    public class UsuarioVM
    {
        [Required]
        public string NombreCompleto { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string PerfilGeneral { get; set; }
        [Required]
        public IFormFile Fotografia { get; set; }
    }
}
