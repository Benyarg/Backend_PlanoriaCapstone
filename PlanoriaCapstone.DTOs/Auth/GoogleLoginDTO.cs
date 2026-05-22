using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Auth
{
    public class GoogleLoginDTO
    {
        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress(ErrorMessage = "El formato del correo es inválido.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, MinimumLength = 1)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El GoogleId es requerido.")]
        public string GoogleId { get; set; } = string.Empty;

        public string? FotoPerfilUrl { get; set; }
    }
}
