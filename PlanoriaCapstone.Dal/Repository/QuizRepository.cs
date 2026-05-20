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
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;

        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }

        // =========================================
        // OBTENER QUIZ COMPLETO
        // =========================================

        public async Task<Quiz?> ObtenerQuizCompletoAsync(
            int idQuiz)
        {
            return await _context.Quizzes
                .Include(q => q.PreguntasQuiz)
                .Include(q => q.AnalisisIA)
                .FirstOrDefaultAsync(q =>
                    q.IdQuiz == idQuiz);
        }

        // =========================================
        // OBTENER POR ID
        // =========================================

        public async Task<Quiz?> ObtenerPorIdAsync(
            int idQuiz)
        {
            return await _context.Quizzes
                .Include(q => q.AnalisisIA)
                .Include(q => q.PreguntasQuiz)
                .FirstOrDefaultAsync(q =>
                    q.IdQuiz == idQuiz);
        }

        // =========================================
        // CREAR HISTORIAL
        // =========================================

        public async Task CrearHistorialAsync(
            HistorialQuiz historial)
        {
            await _context.HistorialQuizzes
                .AddAsync(historial);
        }

        // =========================================
        // GUARDAR CAMBIOS
        // =========================================

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
