using Microsoft.AspNetCore.Http;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.Models;
using BCrypt.Net;

namespace PlanoriaCapstone.Bll.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly IImageService _imageService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IImageService imageService)
        {
            _usuarioRepository = usuarioRepository;

            _imageService = imageService;
        }

        public async Task<Usuario> RegistrarAsync(
            Usuario usuario,
            IFormFile? fotoPerfil)
        {
            var existeCorreo = await _usuarioRepository
                .ObtenerPorCorreoAsync(usuario.Correo);

            if (existeCorreo != null)
            {
                throw new Exception(
                    "El correo ya está registrado.");
            }

            // GOOGLE LOGIN
            if (!string.IsNullOrWhiteSpace(usuario.GoogleId))
            {
                usuario.Provider = "GOOGLE";

                usuario.PasswordHash = null;
            }

            // REGISTRO LOCAL
            else
            {
                usuario.Provider = "LOCAL";

                if (string.IsNullOrWhiteSpace(
                    usuario.PasswordHash))
                {
                    throw new Exception(
                        "La contraseña es requerida.");
                }

                usuario.PasswordHash = BCrypt.Net.BCrypt
                    .HashPassword(usuario.PasswordHash);
            }

            // GUARDAR FOTO PERFIL
            usuario.FotoPerfilUrl = await _imageService
                .GuardarImagenPerfilAsync(fotoPerfil);

            // DATOS INICIALES
            usuario.FechaRegistro = DateTime.UtcNow;

            usuario.UltimoAcceso = DateTime.UtcNow;

            usuario.RachaDias = 0;

            usuario.Puntos = 0;

            usuario.Nivel = 1;

            usuario.Estado = true;

            usuario.IdRol = 2;

            await _usuarioRepository.CrearAsync(usuario);

            await _usuarioRepository.GuardarCambiosAsync();

            return usuario;
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            return await _usuarioRepository
                .ObtenerPorIdAsync(id);
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            return await _usuarioRepository
                .ObtenerTodosAsync();
        }

        public async Task<bool> EditarPerfilAsync(
            int id,
            string nombre,
            string? apellido,
            string correo,
            IFormFile? fotoPerfil)
        {
            var usuario = await _usuarioRepository
                .ObtenerPorIdAsync(id);

            if (usuario == null)
            {
                return false;
            }

            usuario.Nombre = nombre;

            usuario.Apellido = apellido;

            // VALIDAR UNICIDAD DE CORREO SI CAMBIÓ
            if (!string.Equals(
                usuario.Correo, correo,
                StringComparison.OrdinalIgnoreCase))
            {
                var existente = await _usuarioRepository
                    .ObtenerPorCorreoAsync(correo);

                if (existente != null
                    && existente.IdUsuario != id)
                {
                    throw new Exception(
                        "El correo ya está registrado por otro usuario.");
                }

                usuario.Correo = correo;
            }

            // SI SUBE NUEVA FOTO
            if (fotoPerfil != null)
            {
                usuario.FotoPerfilUrl =
                    await _imageService
                        .GuardarImagenPerfilAsync(fotoPerfil);
            }

            await _usuarioRepository
                .ActualizarAsync(usuario);

            await _usuarioRepository
                .GuardarCambiosAsync();

            return true;
        }

        public async Task<bool> CambiarPasswordAsync(
            int id,
            string passwordActual,
            string nuevaPassword)
        {
            var usuario = await _usuarioRepository
                .ObtenerPorIdAsync(id);

            if (usuario == null)
            {
                return false;
            }

            // GOOGLE NO TIENE PASSWORD LOCAL
            if (usuario.Provider == "GOOGLE")
            {
                throw new Exception(
                    "La cuenta Google no tiene contraseña local.");
            }

            // VERIFICAR CONTRASEÑA ACTUAL
            if (string.IsNullOrWhiteSpace(usuario.PasswordHash)
                || !BCrypt.Net.BCrypt.Verify(
                    passwordActual, usuario.PasswordHash))
            {
                throw new Exception(
                    "La contraseña actual es incorrecta.");
            }

            usuario.PasswordHash = BCrypt.Net.BCrypt
                .HashPassword(nuevaPassword);

            await _usuarioRepository
                .ActualizarAsync(usuario);

            await _usuarioRepository
                .GuardarCambiosAsync();

            return true;
        }

        public async Task<bool> EliminarCuentaAsync(int id)
        {
            var usuario = await _usuarioRepository
                .ObtenerPorIdAsync(id);

            if (usuario == null)
            {
                return false;
            }

            // SOFT DELETE
            usuario.Estado = false;

            await _usuarioRepository
                .ActualizarAsync(usuario);

            await _usuarioRepository
                .GuardarCambiosAsync();

            return true;
        }
    }
}
