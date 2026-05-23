using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http.Json;

namespace PlanoriaCapstone.Tests.IntegrationTests
{
    [TestClass]
    public class AuthIntegrationTests
    {
        private static HttpClient _client;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            var factory = new WebApplicationFactory<Program>();

            _client = factory.CreateClient();
        }

        [TestMethod]
        public async Task Register_DeberiaCrearUsuario()
        {
            var usuario = new
            {
                Nombre = "Bryan",
                Correo = "bryan@test.com",
                Password = "Bryan123*"
            };

            var response = await _client.PostAsJsonAsync(
                "/api/auth/register",
                usuario);

            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}