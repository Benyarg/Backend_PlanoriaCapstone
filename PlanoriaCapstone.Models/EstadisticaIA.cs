using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Models
{
    public class EstadisticaIA
    {
        public int IdEstadistica { get; set; }

        public int IdUsuario { get; set; }

        public int TotalArchivosSubidos { get; set; }

        public int TotalFlashcardsGeneradas { get; set; }

        public int TotalQuizzesGenerados { get; set; }

        public decimal? PromedioPuntaje { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
