using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBF.Application.DTOs
{
    public class UsuarioDTO
    {
        public string? NombreUsuario { get; set; }
        public string? Password { get; set; }
        public string? Correo { get; set; }
        public bool IsDeleted { get; set; }
    }
}
