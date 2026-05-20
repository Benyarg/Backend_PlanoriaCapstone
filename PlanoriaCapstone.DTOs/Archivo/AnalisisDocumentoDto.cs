using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanoriaCapstone.DTOs.Archivo
{
    public class AnalisisDocumentoDto
    {
        public string Resumen { get; set; } = string.Empty;

        public List<string> TemasDetectados { get; set; } = new();

        public List<FlashcardDto> Flashcards { get; set; } = new();

        public List<QuizDto> Quizzes { get; set; } = new();
    }

    public class FlashcardDto
    {
        public string Pregunta { get; set; } = string.Empty;
        public string Respuesta { get; set; } = string.Empty;
    }

    public class QuizDto
    {
        public string Pregunta { get; set; } = string.Empty;

        public List<string> Opciones { get; set; } = new();

        public string RespuestaCorrecta { get; set; } = string.Empty;

        public string? Explicacion { get; set; }
    }
}
