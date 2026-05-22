using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class HistorialFlashcard
    {
        public int IdHistorialFlashcard { get; set; }

        public int IdUsuario { get; set; }

        public int IdFlashcard { get; set; }

        public bool Correcta { get; set; }

        public int? TiempoRespuestaSegundos { get; set; }

        public DateTime FechaRespuesta { get; set; }

        public Usuario? Usuario { get; set; }

        public Flashcard? Flashcard { get; set; }
    }
}
