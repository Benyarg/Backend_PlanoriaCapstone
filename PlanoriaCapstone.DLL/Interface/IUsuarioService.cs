using Microsoft.AspNetCore.Http;
using PlanoriaCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IUsuarioService
    {
        Task<Usuario> RegistrarAsync(Usuario usuario, IFormFile? fotoPerfil);

        Task<Usuario?> ObtenerPorIdAsync(int id);

        Task<IEnumerable<Usuario>> ObtenerTodosAsync();

        Task<bool> EditarPerfilAsync(
            int id,
            string nombre,
            string? apellido,
            string correo,
            IFormFile? fotoPerfil);

        Task<bool> CambiarPasswordAsync(
            int id,
            string passwordActual,
            string nuevaPassword);

        Task<bool> EliminarCuentaAsync(int id);
    }
}
