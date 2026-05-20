using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class RachaUsuario
    {
        public int IdRacha { get; set; }

        public int IdUsuario { get; set; }

        public DateTime Fecha { get; set; }

        public int DiasConsecutivos { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
