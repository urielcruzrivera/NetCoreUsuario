using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreUsuario.Models
{
    public class ApiResponseVM
    {
        public bool success { get; set; } = false;
        public string message { get; set; }
        public object data { get; set; }
    }
}
