using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IFlashcardService
    {
        Task<IEnumerable<Flashcard>>
            ObtenerPorArchivoAsync(
                int idAnalisis);

        Task<bool>
            ResponderAsync(
                int idUsuario,
                int idFlashcard,
                bool correcta,
                int tiempoRespuestaSegundos);

        Task<Flashcard?>
            ObtenerPorIdAsync(
                int idFlashcard);
    }
}
