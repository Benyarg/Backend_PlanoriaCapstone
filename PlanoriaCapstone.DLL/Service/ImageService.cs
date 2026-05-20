using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PlanoriaCapstone.Bll.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.Bll.Service
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string?> GuardarImagenPerfilAsync(IFormFile? archivo)
        {
            // SI NO SUBE FOTO
            if (archivo == null || archivo.Length == 0)
            {
                return null;
            }

            // VALIDAR EXTENSIONES
            var extensionesPermitidas = new[]
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".webp"
            };

            var extension = Path.GetExtension(archivo.FileName)
                .ToLower();

            if (!extensionesPermitidas.Contains(extension))
            {
                throw new Exception(
                    "Formato no permitido. Solo JPG, PNG o WEBP.");
            }

            // VALIDAR CONTENT-TYPE
            var contentTypesPermitidos = new[]
            {
                "image/jpeg",
                "image/png",
                "image/webp"
            };

            if (!contentTypesPermitidos.Contains(
                archivo.ContentType.ToLowerInvariant()))
            {
                throw new Exception(
                    "Tipo de contenido no permitido. Solo imágenes JPG, PNG o WEBP.");
            }

            // VALIDAR TAMAÑO
            var tamañoMaximo = 5 * 1024 * 1024;

            if (archivo.Length > tamañoMaximo)
            {
                throw new Exception(
                    "La imagen no puede superar los 5MB.");
            }

            // VALIDAR MAGIC BYTES (firma del archivo)
            if (!await ValidarMagicBytesAsync(archivo))
            {
                throw new Exception(
                    "El contenido del archivo no corresponde a una imagen válida.");
            }

            // CREAR CARPETA SI NO EXISTE
            var carpeta = Path.Combine(
                _environment.WebRootPath,
                "assets",
                "imagesprofile");

            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            // GENERAR NOMBRE ÚNICO
            var nombreArchivo = Guid.NewGuid().ToString()
                                 + extension;

            var rutaCompleta = Path.Combine(
                carpeta,
                nombreArchivo);

            // GUARDAR ARCHIVO
            using (var stream = new FileStream(
                rutaCompleta,
                FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }

            // RETORNAR RUTA RELATIVA
            return $"/assets/imagesprofile/{nombreArchivo}";
        }

        /// <summary>
        /// Valida los primeros bytes del archivo para confirmar
        /// que corresponden a una imagen real (JPG, PNG o WebP).
        /// </summary>
        private static async Task<bool> ValidarMagicBytesAsync(
            IFormFile archivo)
        {
            var buffer = new byte[12];

            using var stream = archivo.OpenReadStream();

            var bytesLeidos = await stream.ReadAsync(
                buffer, 0, buffer.Length);

            if (bytesLeidos < 4) return false;

            // JPEG: FF D8 FF
            if (buffer[0] == 0xFF
                && buffer[1] == 0xD8
                && buffer[2] == 0xFF)
            {
                return true;
            }

            // PNG: 89 50 4E 47
            if (buffer[0] == 0x89
                && buffer[1] == 0x50
                && buffer[2] == 0x4E
                && buffer[3] == 0x47)
            {
                return true;
            }

            // WebP: RIFF....WEBP
            if (bytesLeidos >= 12
                && buffer[0] == 0x52   // R
                && buffer[1] == 0x49   // I
                && buffer[2] == 0x46   // F
                && buffer[3] == 0x46   // F
                && buffer[8] == 0x57   // W
                && buffer[9] == 0x45   // E
                && buffer[10] == 0x42  // B
                && buffer[11] == 0x50) // P
            {
                return true;
            }

            return false;
        }
    }
}
