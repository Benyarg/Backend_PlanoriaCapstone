using PlanoriaCapstone.DTOs.Archivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IIAService
    {
        Task<AnalisisDocumentoDto> AnalizarTextoAsync(string texto);
    }
}
