using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progreso
{
    public class ProgresoResponseDTO
    {
        public int IdProgresoArchivo { get; set; }
        public int IdUsuario { get; set; }
        public int IdArchivo { get; set; }
        public int FlashcardsCompletadas { get; set; }
        public int FlashcardsTotales { get; set; }
        public int QuizzesCompletados { get; set; }
        public int QuizzesTotales { get; set; }
        public decimal PorcentajeProgreso { get; set; }
        public decimal PromedioPuntaje { get; set; }
        public bool Completado { get; set; }
        public DateTime? UltimaSesion { get; set; }
    }
}
