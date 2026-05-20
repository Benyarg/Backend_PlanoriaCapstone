using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Bll.Service
{
    public class ProgresoService
         : IProgresoService
    {
        private readonly IProgresoRepository
            _repository;

        public ProgresoService(
            IProgresoRepository repository)
        {
            _repository = repository;
        }

        // =========================================
        // OBTENER PROGRESO
        // =========================================

        public async Task<ProgresoArchivo>
            ObtenerProgresoAsync(
                int idUsuario,
                int idArchivo)
        {
            var progreso =
                await _repository.ObtenerAsync(
                    idUsuario,
                    idArchivo);

            // SI NO EXISTE -> CREAR AUTOMÁTICAMENTE
            if (progreso == null)
            {
                var totalFlashcards =
                    await _repository
                    .ObtenerTotalFlashcardsAsync(
                        idArchivo);

                var totalQuizzes =
                    await _repository
                    .ObtenerTotalQuizzesAsync(
                        idArchivo);

                progreso = new ProgresoArchivo
                {
                    IdUsuario = idUsuario,
                    IdArchivo = idArchivo,

                    FlashcardsCompletadas = 0,
                    FlashcardsTotales = totalFlashcards,

                    QuizzesCompletados = 0,
                    QuizzesTotales = totalQuizzes,

                    PorcentajeProgreso = 0,

                    Completado = false,

                    UltimaSesion = DateTime.Now
                };

                await _repository
                    .CrearAsync(progreso);

                await _repository
                    .GuardarCambiosAsync();
            }

            return progreso;
        }

        // =========================================
        // OBTENER TODOS LOS PROGRESOS
        // =========================================

        public async Task<List<ProgresoArchivo>>
            ObtenerTodosUsuarioAsync(
                int idUsuario)
        {
            return await _repository
                .ObtenerTodosUsuarioAsync(
                    idUsuario);
        }

        // =========================================
        // PROMEDIO DE PUNTAJE
        // =========================================

        public async Task<decimal>
        ObtenerPromedioQuizAsync(
         int idUsuario,
         int idArchivo)
        {
            return await _repository
                .ObtenerPromedioPuntajeAsync(
                    idUsuario,
                    idArchivo);
        }

        // =========================================
        // ACTUALIZAR PROGRESO
        // =========================================

        public async Task ActualizarProgresoAsync(
            int idUsuario,
            int idArchivo,
            int flashcardsCompletadas,
            int quizzesCompletados)
        {
            var progreso =
                await _repository.ObtenerAsync(
                    idUsuario,
                    idArchivo);

            // SI NO EXISTE
            if (progreso == null)
            {
                var totalFlashcards =
                    await _repository
                    .ObtenerTotalFlashcardsAsync(
                        idArchivo);

                var totalQuizzes =
                    await _repository
                    .ObtenerTotalQuizzesAsync(
                        idArchivo);

                progreso = new ProgresoArchivo
                {
                    IdUsuario = idUsuario,
                    IdArchivo = idArchivo,

                    FlashcardsCompletadas =
                        flashcardsCompletadas,

                    FlashcardsTotales =
                        totalFlashcards,

                    QuizzesCompletados =
                        quizzesCompletados,

                    QuizzesTotales =
                        totalQuizzes,

                    UltimaSesion =
                        DateTime.Now
                };

                progreso.PorcentajeProgreso =
                    CalcularPorcentaje(
                        progreso);

                progreso.Completado =
                    progreso.PorcentajeProgreso
                    >= 100;

                await _repository
                    .CrearAsync(progreso);
            }
            else
            {
                progreso.FlashcardsCompletadas =
                    flashcardsCompletadas;

                progreso.QuizzesCompletados =
                    quizzesCompletados;

                progreso.UltimaSesion =
                    DateTime.Now;

                progreso.PorcentajeProgreso =
                    CalcularPorcentaje(
                        progreso);

                progreso.Completado =
                    progreso.PorcentajeProgreso
                    >= 100;

                await _repository
                    .ActualizarAsync(progreso);
            }

            await _repository
                .GuardarCambiosAsync();
        }

        // =========================================
        // CALCULAR PORCENTAJE
        // =========================================

        private decimal CalcularPorcentaje(
            ProgresoArchivo progreso)
        {
            decimal total =
                progreso.FlashcardsTotales +
                progreso.QuizzesTotales;

            decimal completado =
                progreso.FlashcardsCompletadas +
                progreso.QuizzesCompletados;

            if (total == 0)
                return 0;

            return Math.Round(
                (completado / total) * 100,
                2);
        }
    }
}
