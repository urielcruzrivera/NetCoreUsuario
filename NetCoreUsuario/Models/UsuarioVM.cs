using System.ComponentModel.DataAnnotations;

namespace NetCoreUsuario.Models
{
    public class UsuarioVM
    {
        public int id { get; set; }
        [Required]
        public string NombreCompleto { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int PerfilGeneral { get; set; }
    }
}
