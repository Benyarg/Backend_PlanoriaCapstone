using Microsoft.AspNetCore.Http;
using PlanoriaCapstone.Bll.Interface;

namespace PlanoriaCapstone.Bll.Service
{
    public class ImageService : IImageService
    {
        public async Task<string> GuardarImagenAsync(
            IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                return string.Empty;

            // Carpeta uploads
            var carpeta =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads");

            // Crear carpeta si no existe
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            // Nombre único
            var nombreArchivo =
                Guid.NewGuid().ToString() +
                Path.GetExtension(archivo.FileName);

            var rutaCompleta =
                Path.Combine(
                    carpeta,
                    nombreArchivo);

            // Guardar archivo
            using (var stream =
                   new FileStream(
                       rutaCompleta,
                       FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // URL relativa
            return "/uploads/" + nombreArchivo;
        }
    }
}