using Microsoft.AspNetCore.Http;

namespace PlanoriaCapstone.Bll.Interface
{
    public interface IImageService
    {
        Task<string> GuardarImagenAsync(
            IFormFile archivo);
    }
}