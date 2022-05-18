using System.ComponentModel.DataAnnotations;

namespace NetCoreUsuario.Models
{
    public class UsuarioVM
    {
        [Required]
        public string NombreCompleto { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int PerfilGeneral { get; set; }
    }
}
