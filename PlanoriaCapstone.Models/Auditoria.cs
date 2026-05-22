using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class Auditoria
    {
        public int IdAuditoria { get; set; }

        public int? IdUsuario { get; set; }

        public string Accion { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public DateTime Fecha { get; set; }

        public string? IpAddress { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
