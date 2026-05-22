using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz
{
    public class PreguntaQuizDTO
    {
        public int IdPreguntaQuiz { get; set; }

        public string Pregunta { get; set; }
            = string.Empty;

        public string OpcionA { get; set; }
            = string.Empty;

        public string OpcionB { get; set; }
            = string.Empty;

        public string? OpcionC { get; set; }

        public string? OpcionD { get; set; }
    }
}
