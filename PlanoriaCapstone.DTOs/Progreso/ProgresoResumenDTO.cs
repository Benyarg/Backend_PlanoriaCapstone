using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Progreso
{
    // DTO QUE SERIVIRIA PARA DASHBOARD
    public class ProgresoResumenDTO
    {
        public int TotalArchivos { get; set; }

        public int TotalCompletados { get; set; }

        public decimal PromedioGeneral { get; set; }
    }
}
