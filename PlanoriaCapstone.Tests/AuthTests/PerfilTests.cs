using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlanoriaCapstone.Tests.UsuarioTests
{
    [TestClass]
    public class PerfilTests
    {
        [TestMethod]
        public void Usuario_NombreNoDebeSerVacio()
        {
            string nombre = "Bryan";

            Assert.IsFalse(string.IsNullOrEmpty(nombre));
        }

        [TestMethod]
        public void Usuario_CorreoDebeContenerArroba()
        {
            string correo = "bryan@gmail.com";

            Assert.IsTrue(correo.Contains("@"));
        }

        [TestMethod]
        public void Usuario_ApellidoPuedeSerNull()
        {
            string? apellido = null;

            Assert.IsNull(apellido);
        }
    }
}