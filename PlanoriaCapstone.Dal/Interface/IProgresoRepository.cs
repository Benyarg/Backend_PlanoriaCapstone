using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Dal.Interface
{
    public interface IProgresoRepository
    {
        Task<ProgresoArchivo?> ObtenerAsync(
            int idUsuario,
            int idArchivo);

        Task<List<ProgresoArchivo>>
            ObtenerTodosUsuarioAsync(
                int idUsuario);

        Task<int>
            ObtenerTotalFlashcardsAsync(
                int idArchivo);

        Task<int>
            ObtenerTotalQuizzesAsync(
                int idArchivo);

        Task<decimal>
            ObtenerPromedioPuntajeAsync(
                int idUsuario,
                int idArchivo);

        Task CrearAsync(
            ProgresoArchivo progreso);

        Task ActualizarAsync(
            ProgresoArchivo progreso);

        Task GuardarCambiosAsync();
    }
}
