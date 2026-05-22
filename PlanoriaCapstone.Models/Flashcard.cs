using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class Flashcard
    {
        public int IdFlashcard { get; set; }

        public int IdAnalisis { get; set; }

        public string Pregunta { get; set; } = string.Empty;

        public string Respuesta { get; set; } = string.Empty;

        public string? NivelDificultad { get; set; }

        public int VecesEstudiada { get; set; }

        public DateTime FechaCreacion { get; set; }

        public AnalisisIA? AnalisisIA { get; set; }

        public ICollection<HistorialFlashcard>? HistorialFlashcards { get; set; }
    }
}
