using Microsoft.EntityFrameworkCore;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Dal.Repository
{
    public class ProgresoRepository
        : IProgresoRepository
    {
        private readonly AppDbContext _context;

        public ProgresoRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProgresoArchivo?>
            ObtenerAsync(
                int idUsuario,
                int idArchivo)
        {
            return await _context.ProgresoArchivos
                .Include(p => p.Usuario)
                .Include(p => p.ArchivoSubido)
                .FirstOrDefaultAsync(p =>
                    p.IdUsuario == idUsuario &&
                    p.IdArchivo == idArchivo);
        }

        public async Task<List<ProgresoArchivo>>
            ObtenerTodosUsuarioAsync(
                int idUsuario)
        {
            return await _context.ProgresoArchivos
                .Include(p => p.ArchivoSubido)
                .Where(p => p.IdUsuario == idUsuario)
                .OrderByDescending(p => p.UltimaSesion)
                .ToListAsync();
        }

        public async Task<int>
            ObtenerTotalFlashcardsAsync(
                int idArchivo)
        {
            return await _context.Flashcards
                .CountAsync(f =>
                    f.AnalisisIA!.IdArchivo ==
                    idArchivo);
        }

        public async Task<int>
            ObtenerTotalQuizzesAsync(
                int idArchivo)
        {
            return await _context.Quizzes
                .CountAsync(q =>
                    q.AnalisisIA!.IdArchivo ==
                    idArchivo);
        }

        public async Task<decimal>
            ObtenerPromedioPuntajeAsync(
                int idUsuario,
                int idArchivo)
        {
            var quizIds = await _context.Quizzes
                .Where(q =>
                    q.AnalisisIA!.IdArchivo ==
                    idArchivo)
                .Select(q => q.IdQuiz)
                .ToListAsync();

            var promedio =
                await _context.HistorialQuizzes
                .Where(h =>
                    h.IdUsuario == idUsuario &&
                    quizIds.Contains(h.IdQuiz))
                .AverageAsync(h =>
                    (decimal?)h.Puntaje);

            return promedio ?? 0;
        }

        public async Task CrearAsync(
            ProgresoArchivo progreso)
        {
            await _context.ProgresoArchivos
                .AddAsync(progreso);
        }

        public async Task ActualizarAsync(
            ProgresoArchivo progreso)
        {
            _context.ProgresoArchivos
                .Update(progreso);

            await Task.CompletedTask;
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
