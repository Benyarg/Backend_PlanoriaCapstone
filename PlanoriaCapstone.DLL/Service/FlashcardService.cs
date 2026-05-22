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
    public class FlashcardService
         : IFlashcardService
    {
        private readonly IFlashcardRepository
            _repository;

        public FlashcardService(
            IFlashcardRepository repository)
        {
            _repository = repository;
        }

        // =====================================
        // OBTENER FLASHCARDS
        // =====================================

        public async Task<IEnumerable<Flashcard>>
            ObtenerPorArchivoAsync(
                int idAnalisis)
        {
            return await _repository
                .ObtenerPorArchivoAsync(
                    idAnalisis);
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
            return await _repository
                .ResponderAsync(
                    idUsuario,
                    idFlashcard,
                    correcta,
                    tiempoRespuestaSegundos);
        }

        // =====================================
        // OBTENER FLASHCARD POR ID
        // =====================================

        public async Task<Flashcard?>
            ObtenerPorIdAsync(
                int idFlashcard)
        {
            return await _repository
                .ObtenerPorIdAsync(
                    idFlashcard);
        }
    }
}
