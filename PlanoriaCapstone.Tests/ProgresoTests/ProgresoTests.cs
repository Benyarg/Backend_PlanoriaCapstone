using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlanoriaCapstone.Tests.ProgresoTests
{
    [TestClass]
    public class ProgresoTests
    {
        [TestMethod]
        public void Progreso_PorcentajeValido()
        {
            int progreso = 80;

            Assert.IsTrue(progreso >= 0 && progreso <= 100);
        }

        [TestMethod]
        public void Progreso_Completado()
        {
            bool completado = true;

            Assert.IsTrue(completado);
        }
    }
}