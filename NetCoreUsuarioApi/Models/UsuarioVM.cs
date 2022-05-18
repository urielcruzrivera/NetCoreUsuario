using System;

namespace NetCoreUsuarioApi.Models
{
    public class UsuarioVM
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public int PerfilGeneral { get; set; }
        public string PerfilDescripcion { get; set; }
        public string FechaCreacion { get; set; }
    }
}
