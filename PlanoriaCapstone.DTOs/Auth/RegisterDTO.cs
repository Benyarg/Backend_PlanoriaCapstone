using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Auth
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(100,
            ErrorMessage = "El apellido no puede superar 100 caracteres.")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress(ErrorMessage = "El formato del correo es inválido.")]
        [StringLength(150,
            ErrorMessage = "El correo no puede superar 150 caracteres.")]
        public string Correo { get; set; } = string.Empty;

        [MinLength(8, ErrorMessage = "La contraseña debe tener mínimo 8 caracteres.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
            ErrorMessage = "Debe tener mayúscula, minúscula, número y carácter especial.")]
        public string? Password { get; set; }

        // GOOGLE
        public string? GoogleId { get; set; }

        public string? FotoPerfilUrl { get; set; }
    }
}
