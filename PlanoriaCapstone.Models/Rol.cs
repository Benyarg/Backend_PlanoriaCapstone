using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class Rol
    {
        public int IdRol { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
