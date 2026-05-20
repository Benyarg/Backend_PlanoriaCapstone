using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class HistorialQuiz
    {
        public int IdHistorialQuiz { get; set; }

        public int IdUsuario { get; set; }

        public int IdQuiz { get; set; }

        public decimal Puntaje { get; set; }

        public int CantidadCorrectas { get; set; }

        public int CantidadIncorrectas { get; set; }

        public int? TiempoResolucionMinutos { get; set; }

        public DateTime FechaRealizacion { get; set; }

        public Usuario? Usuario { get; set; }

        public Quiz? Quiz { get; set; }
    }
}
