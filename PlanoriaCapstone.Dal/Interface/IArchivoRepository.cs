using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Dal.Interface
{
    public interface IArchivoRepository
    {
        Task CrearAsync(ArchivoSubido archivo);

        Task ActualizarAsync(ArchivoSubido archivo);

        Task EliminarAsync(ArchivoSubido archivo);

        Task<ArchivoSubido?> ObtenerPorIdAsync(int idArchivo);

        Task<IEnumerable<ArchivoSubido>> ObtenerPorUsuarioAsync(int idUsuario);

        Task GuardarCambiosAsync();
    }
}
