using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Dal.Interface
{
    public interface IQuizRepository
    {
        Task<Quiz?> ObtenerQuizCompletoAsync(
            int idQuiz);

        Task<Quiz?> ObtenerPorIdAsync(
            int idQuiz);

        Task CrearHistorialAsync(
            HistorialQuiz historial);

        Task GuardarCambiosAsync();
    }
}
