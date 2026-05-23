using Microsoft.AspNetCore.Http;
using PlanoriaCapstone.Models;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IUsuarioService
    {
        Task<Usuario?> ObtenerPorIdAsync(int id);

        Task<Usuario> RegistrarAsync(
            Usuario usuario,
            IFormFile? fotoPerfil);

        // NUEVOS MÉTODOS

        Task<bool> EditarPerfilAsync(
            int idUsuario,
            string nombre,
            string? apellido,
            string correo,
            IFormFile? fotoPerfil);

        Task<bool> CambiarPasswordAsync(
            int idUsuario,
            string passwordActual,
            string nuevaPassword);

        Task<bool> EliminarCuentaAsync(
            int idUsuario);
    }
}