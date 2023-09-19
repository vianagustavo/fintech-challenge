using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FintechChallenge.Models;
using FintechChallenge.Tests.Helpers;
using Xunit;

namespace FintechChallenge.Tests.Integration
{
    public class PeopleLoginControllerIntegrationTests : IClassFixture<IntegrationsTestFactory>
    {
        private readonly HttpClient _client;

        public PeopleLoginControllerIntegrationTests(IntegrationsTestFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task LoginPeople_ValidCredentials_ReturnsOkWithToken()
        {
            var createRequest = new CreatePeopleRequest("Test Name", "12345678901", "1234");

            var mockedUserFactory = new CreatePeopleMockFactory();
            await mockedUserFactory.MockPeople(createRequest, _client);

            var validLoginRequest = new PeopleLoginRequest("12345678901", "1234");

            var mockedLoginFactory = new LoginPeopleMockFactory();
            var loginResponseObject = await mockedLoginFactory.MockLogin(validLoginRequest, _client);

            Assert.NotNull(loginResponseObject);
            Assert.NotNull(loginResponseObject.Token);
        }

        [Fact]
        public async Task LoginPeople_InvalidCredentials_ReturnsUnauthorized()
        {
            var invalidRequest = new PeopleLoginRequest("invalidUsername", "invalidPassword");

            var jsonRequest = JsonSerializer.Serialize(invalidRequest);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/people/login", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}