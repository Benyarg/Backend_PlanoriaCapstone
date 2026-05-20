using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz
{
    public class ResolverQuizDTO
    {
        public int IdUsuario { get; set; }

        public int IdQuiz { get; set; }

        public List<RespuestaPreguntaDTO> Respuestas
        { get; set; } = new();
    }

    public class RespuestaPreguntaDTO
    {
        public int IdPreguntaQuiz { get; set; }

        public string Respuesta { get; set; }
            = string.Empty;
    }
}
