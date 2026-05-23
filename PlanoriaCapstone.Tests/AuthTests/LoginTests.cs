using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlanoriaCapstone.Tests.AuthTests
{
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        public void Login_CorreoValido_DeberiaSerTrue()
        {
            string correo = "bryan@gmail.com";

            bool valido = correo.Contains("@");

            Assert.IsTrue(valido);
        }

        [TestMethod]
        public void Login_PasswordMayor8_DeberiaSerTrue()
        {
            string password = "Bryan123*";

            bool valido = password.Length >= 8;

            Assert.IsTrue(valido);
        }

        [TestMethod]
        public void Login_PasswordVacio_DeberiaSerFalse()
        {
            string password = "";

            bool valido = string.IsNullOrEmpty(password);

            Assert.IsTrue(valido);
        }
    }
}