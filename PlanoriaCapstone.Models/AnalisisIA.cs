using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class AnalisisIA
    {
        public int IdAnalisis { get; set; }

        public int IdArchivo { get; set; }

        public string? Resumen { get; set; }

        public string? TemasDetectados { get; set; }

        public string EstadoProceso { get; set; } = "PROCESANDO";

        public DateTime FechaAnalisis { get; set; }

        public ArchivoSubido? ArchivoSubido { get; set; }

        public ICollection<Flashcard>? Flashcards { get; set; }

        public ICollection<Quiz>? Quizzes { get; set; }
    }
}
