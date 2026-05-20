using Microsoft.Extensions.Configuration;
using PlanoriaCapstone.Bll.Interface;
using PlanoriaCapstone.DTOs.Archivo;
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
            _apiKey = configuration["Gemini:ApiKey"];
        }

        public async Task<AnalisisDocumentoDto> AnalizarTextoAsync(string texto)
        {
            // Nota: En mayo de 2026, gemini-1.5-flash ha sido reemplazado.
            // Usamos 'gemini-flash-latest' que es un alias estable.
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-flash-latest:generateContent";

            var prompt = $@"
Eres un sistema educativo avanzado. Analiza el texto proporcionado y genera un resumen, temas clave, flashcards (pregunta/respuesta) y un quiz.

Devuelve SOLO JSON válido con esta estructura:
{{
  ""resumen"": ""string"",
  ""temasDetectados"": [""string""],
  ""flashcards"": [
    {{
      ""pregunta"": ""string"",
      ""respuesta"": ""string""
    }}
  ],
  ""quizzes"": [
    {{
      ""pregunta"": ""string"",
      ""opciones"": [""A"",""B"",""C"",""D""],
      ""respuestaCorrecta"": ""A"",
      ""explicacion"": ""string""
    }}
  ]
}}

REGLAS DE LONGITUD (MUY IMPORTANTE):
- resumen: Máximo 500 caracteres.
- temasDetectados: Máximo 5 temas, cada uno máximo 30 caracteres.
- flashcards: Máximo 5 pares. Pregunta y respuesta máximo 150 caracteres cada una.
- quizzes: Máximo 5 preguntas. 
  * Pregunta: Máximo 200 caracteres.
  * Opciones: Máximo 100 caracteres cada una.
  * Explicación: Máximo 200 caracteres.

REGLAS GENERALES:
- SOLO JSON.
- Todo el contenido en Español.
- El texto a analizar es:
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
            {
                raw = raw.Replace("```json", "").Replace("```", "").Trim();
            }

            return JsonSerializer.Deserialize<AnalisisDocumentoDto>(
                       raw!,
                       new JsonSerializerOptions
                       {
                           PropertyNameCaseInsensitive = true
                       })
                   ?? new AnalisisDocumentoDto();
        }
    }
}
