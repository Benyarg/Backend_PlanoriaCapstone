using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> ObtenerPorArchivoAsync(
            int idArchivo);

        Task<Quiz?> ObtenerPorIdAsync(
            int idQuiz);

        Task<bool> ResolverQuizAsync(
            int idUsuario,
            int idQuiz,
            int correctas,
            int incorrectas,
            decimal puntaje,
            int tiempoResolucionMinutos);
    }
}
