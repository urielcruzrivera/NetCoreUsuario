using System;

namespace Model.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public int PerfilGeneral { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
