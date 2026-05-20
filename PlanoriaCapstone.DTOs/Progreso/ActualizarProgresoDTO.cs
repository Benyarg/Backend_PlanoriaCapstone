using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progreso
{
    public class ActualizarProgresoDTO
    {
        public int IdUsuario { get; set; }

        public int IdArchivo { get; set; }

        public int FlashcardsCompletadas { get; set; }

        public int QuizzesCompletados { get; set; }
    }
}
