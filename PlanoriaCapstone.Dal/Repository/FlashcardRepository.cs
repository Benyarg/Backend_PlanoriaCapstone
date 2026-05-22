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
    public class FlashcardRepository
       : IFlashcardRepository
    {
        private readonly AppDbContext
            _context;

        public FlashcardRepository(
            AppDbContext context)
        {
            _context = context;
        }

        // =====================================
        // OBTENER FLASHCARDS
        // =====================================

        public async Task<IEnumerable<Flashcard>>
            ObtenerPorArchivoAsync(
                int idAnalisis)
        {
            return await _context.Flashcards
                .Where(f =>
                    f.IdAnalisis == idAnalisis)
                .ToListAsync();
        }

        // =====================================
        // RESPONDER FLASHCARD
        // =====================================

        public async Task<bool>
            ResponderAsync(
                int idUsuario,
                int idFlashcard,
                bool correcta,
                int tiempoRespuestaSegundos)
        {
            var flashcard =
                await _context.Flashcards
                    .FirstOrDefaultAsync(f =>
                        f.IdFlashcard == idFlashcard);

            if (flashcard == null)
                return false;

            // Aquí puedes guardar historial después

            await _context.SaveChangesAsync();

            return true;
        }

        // =====================================
        // OBTENER POR ID
        // =====================================

        public async Task<Flashcard?>
            ObtenerPorIdAsync(
                int idFlashcard)
        {
            return await _context.Flashcards
                .Include(f => f.AnalisisIA)
                .FirstOrDefaultAsync(f =>
                    f.IdFlashcard == idFlashcard);
        }
    }
}
