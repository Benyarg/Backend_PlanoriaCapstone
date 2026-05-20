using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Dal.Interface
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerPorIdAsync(int id);

        Task<Usuario?> ObtenerPorCorreoAsync(string correo);

        Task<IEnumerable<Usuario>> ObtenerTodosAsync();

        Task CrearAsync(Usuario usuario);

        Task ActualizarAsync(Usuario usuario);

        Task EliminarAsync(Usuario usuario);

        Task GuardarCambiosAsync();
    }
}
