using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Response
{
    public class UsuarioResponseDTO
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Apellido { get; set; }

        public string Correo { get; set; } = string.Empty;

        public string Provider { get; set; } = string.Empty;

        public string? FotoPerfilUrl { get; set; }

        public int RachaDias { get; set; }

        public int Puntos { get; set; }

        public int Nivel { get; set; }
    }
}
