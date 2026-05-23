using Microsoft.Extensions.Configuration;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Archivo;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PlanoriaCapstone.Bll.Service
{
    public class GeminiService : IIAService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GeminiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();

            _apiKey = configuration["Gemini:ApiKey"]
          ?? Environment.GetEnvironmentVariable("GEMINI_API_KEY")
          ?? throw new ArgumentNullException(
              "Gemini API Key no configurada.");
        }

        public async Task<AnalisisDocumentoDto> AnalizarTextoAsync(string texto)
        {
            // URL estable de Gemini
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent";

            var prompt = $@"
Eres un sistema educativo avanzado. Analiza el texto proporcionado y genera un resumen, temas clave, flashcards y un quiz en JSON válido.

Texto a analizar:
{texto}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("x-goog-api-key", _apiKey);
            request.Content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error de Gemini API ({response.StatusCode}): {json}");
            }

            using var doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("candidates", out var candidates) || candidates.GetArrayLength() == 0)
            {
                throw new Exception("La IA no devolvió ninguna respuesta válida.");
            }

            var raw = candidates[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            if (!string.IsNullOrEmpty(raw))
                raw = raw.Replace("```json", "").Replace("```", "").Trim();

            return JsonSerializer.Deserialize<AnalisisDocumentoDto>(
                       raw!,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                   ) ?? new AnalisisDocumentoDto();
        }
    }
}