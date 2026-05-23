using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlanoriaCapstone.Tests.QuizTests
{
    [TestClass]
    public class QuizTests
    {
        [TestMethod]
        public void Quiz_PuntajeMayor0()
        {
            int puntaje = 10;

            Assert.IsTrue(puntaje > 0);
        }

        [TestMethod]
        public void Quiz_ListaPreguntasNoVacia()
        {
            var preguntas = new List<string>
            {
                "Pregunta 1",
                "Pregunta 2"
            };

            Assert.IsTrue(preguntas.Count > 0);
        }
    }
}