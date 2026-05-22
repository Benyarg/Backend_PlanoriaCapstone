using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class Quiz
    {
        public int IdQuiz { get; set; }

        public int IdAnalisis { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public AnalisisIA? AnalisisIA { get; set; }

        public ICollection<PreguntaQuiz>? PreguntasQuiz { get; set; }

        public ICollection<HistorialQuiz>? HistorialQuizzes { get; set; }
    }
}
