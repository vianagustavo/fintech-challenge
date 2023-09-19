using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FintechChallenge.Models;
using Xunit;

namespace FintechChallenge.Tests.Helpers
{
    public class CreatePeopleMockFactory
    {
        public async Task<CreatePeopleResponse> MockPeople(CreatePeopleRequest request, HttpClient client)
        {
            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/people", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var createPeopleResponse = JsonSerializer.Deserialize<CreatePeopleResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return createPeopleResponse;
        }
    }
}

