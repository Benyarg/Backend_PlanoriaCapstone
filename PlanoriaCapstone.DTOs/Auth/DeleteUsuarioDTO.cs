using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Auth
{
    public class DeleteUsuarioDTO
    {
        [Required(ErrorMessage = "Debe confirmar su correo.")]
        [EmailAddress(ErrorMessage = "El formato del correo es inválido.")]
        public string CorreoConfirmacion { get; set; } = string.Empty;
    }
}
