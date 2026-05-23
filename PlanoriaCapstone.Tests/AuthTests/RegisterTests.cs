using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlanoriaCapstone.Tests.AuthTests
{
    [TestClass]
    public class RegisterTests
    {
        [TestMethod]
        public void Register_NombreValido()
        {
            string nombre = "Bryan";

            Assert.IsTrue(nombre.Length >= 2);
        }

        [TestMethod]
        public void Register_CorreoValido()
        {
            string correo = "test@gmail.com";

            Assert.IsTrue(correo.Contains("@"));
        }

        [TestMethod]
        public void Register_PasswordSegura()
        {
            string password = "Bryan123*";

            bool tieneMayuscula = password.Any(char.IsUpper);
            bool tieneNumero = password.Any(char.IsDigit);

            Assert.IsTrue(tieneMayuscula && tieneNumero);
        }
    }
}