using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Flashcard
{
    public class FlashcardResponseDTO
    {
        public int IdFlashcard { get; set; }

        public string Pregunta { get; set; } = string.Empty;

        public string Respuesta { get; set; } = string.Empty;

        public string? NivelDificultad { get; set; }

        public int VecesEstudiada { get; set; }
    }
}
