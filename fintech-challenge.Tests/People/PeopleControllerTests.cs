using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FintechChallenge.Models;
using Xunit;

namespace FintechChallenge.Tests.Integration
{
    public class PeopleControllerIntegrationTests : IClassFixture<IntegrationsTestFactory>
    {
        private readonly HttpClient _client;

        public PeopleControllerIntegrationTests(IntegrationsTestFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreatePeople_ValidRequest_ReturnsCreated()
        {
            var createRequest = new CreatePeopleRequest("Test Name", "1234", "1234567890");


            var jsonRequest = JsonSerializer.Serialize(createRequest);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/people", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var createPeopleResponse = JsonSerializer.Deserialize<CreatePeopleResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(createPeopleResponse);
            Assert.NotEqual(Guid.Empty, createPeopleResponse.Id);
            Assert.Equal("Test Name", createPeopleResponse.Name);
        }
    }
}