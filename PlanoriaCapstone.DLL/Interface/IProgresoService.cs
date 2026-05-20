using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IProgresoService
    {
        // =========================================
        // OBTENER PROGRESO POR ARCHIVO
        // =========================================

        Task<ProgresoArchivo>
            ObtenerProgresoAsync(
                int idUsuario,
                int idArchivo);

        // =========================================
        // OBTENER TODOS LOS PROGRESOS
        // =========================================

        Task<List<ProgresoArchivo>>
            ObtenerTodosUsuarioAsync(
                int idUsuario);

        // =========================================
        // PROMEDIO QUIZZES
        // =========================================

        Task<decimal>
            ObtenerPromedioQuizAsync(
                int idUsuario,
                int idArchivo);

        // =========================================
        // ACTUALIZAR PROGRESO
        // =========================================

        Task ActualizarProgresoAsync(
            int idUsuario,
            int idArchivo,
            int flashcardsCompletadas,
            int quizzesCompletados);
    }
}
