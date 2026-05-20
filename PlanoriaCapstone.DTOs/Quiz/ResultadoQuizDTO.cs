using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Quiz
{
    public class ResultadoQuizDTO
    {
        public decimal Puntaje { get; set; }

        public int Correctas { get; set; }

        public int Incorrectas { get; set; }

        public string Mensaje { get; set; }
            = string.Empty;
    }
}
