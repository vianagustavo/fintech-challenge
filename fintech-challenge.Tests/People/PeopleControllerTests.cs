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
            // Arrange
            var createRequest = new CreatePeopleRequest("John Doe", "P@ssw0rd", "1234567890");


            var jsonRequest = JsonSerializer.Serialize(createRequest);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/people", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            // Deserialize the response content to check the result
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var createPeopleResponse = JsonSerializer.Deserialize<CreatePeopleResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Additional assertions based on the response
            Assert.NotNull(createPeopleResponse);
            Assert.NotEqual(Guid.Empty, createPeopleResponse.Id);
            Assert.Equal("John Doe", createPeopleResponse.Name);
            // Add more assertions as needed
        }
    }
}