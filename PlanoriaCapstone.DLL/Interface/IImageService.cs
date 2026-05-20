using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IImageService
    {
        Task<string?> GuardarImagenPerfilAsync(IFormFile? archivo);
    }
}
