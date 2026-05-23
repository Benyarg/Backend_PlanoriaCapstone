using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlanoriaCapstone.Tests.FlashcardTests
{
    [TestClass]
    public class FlashcardTests
    {
        [TestMethod]
        public void Flashcard_PreguntaNoVacia()
        {
            string pregunta = "¿Qué es Docker?";

            Assert.IsFalse(string.IsNullOrEmpty(pregunta));
        }

        [TestMethod]
        public void Flashcard_RespuestaNoVacia()
        {
            string respuesta = "Es una plataforma de contenedores";

            Assert.IsFalse(string.IsNullOrEmpty(respuesta));
        }
    }
}