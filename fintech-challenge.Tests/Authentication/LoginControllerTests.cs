using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FintechChallenge.Models;
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

            var createPeopleJsonRequest = JsonSerializer.Serialize(createRequest);
            var createPeopleContent = new StringContent(createPeopleJsonRequest, Encoding.UTF8, "application/json");

            await _client.PostAsync("/people", createPeopleContent);

            var validRequest = new PeopleLoginRequest("12345678901", "1234");

            var jsonRequest = JsonSerializer.Serialize(validRequest);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/people/login", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<PeopleLoginResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(responseObject);
            Assert.NotNull(responseObject.Token);
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