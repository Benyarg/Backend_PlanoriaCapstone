using Microsoft.AspNetCore.Mvc;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.Dal.Interface;
using PlanoriaCapstone.DTOs.Auth;
using System.Security.Claims;

namespace Backend_PlanoriaCapstone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtService _jwtService;

        public AuthController(
            IAuthService authService,
            IUsuarioRepository usuarioRepository,
            IJwtService jwtService)
        {
            _authService = authService;
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var usuario = await _usuarioRepository
                .ObtenerPorCorreoAsync(dto.Correo);

            if (usuario == null || !usuario.Estado)
                return Unauthorized("Credenciales inválidas");

            if (usuario.Provider == "GOOGLE")
                return BadRequest("Use login con Google");

            var passwordValida = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                usuario.PasswordHash
            );

            if (!passwordValida)
                return Unauthorized("Credenciales inválidas");

            var token = _jwtService.GenerarToken(usuario);

            return Ok(new AuthResponseDTO
            {
                Token = token,
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.IdRol == 1 ? "ADMIN" : "USER"
            });
        }

        // ME (usuario autenticado)
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (idClaim == null)
                return Unauthorized();

            var usuario = await _usuarioRepository
                .ObtenerPorIdAsync(int.Parse(idClaim));

            if (usuario == null)
                return NotFound();

            return Ok(new
            {
                usuario.IdUsuario,
                usuario.Nombre,
                usuario.Correo,
                Rol = usuario.IdRol == 1 ? "ADMIN" : "USER"
            });
        }
    }
}
