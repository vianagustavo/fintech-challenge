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
    public class CreateCardMockFactory
    {
        public async Task<CreateCardResponse> MockCreateCard(CreateCardRequest request, Guid accountId, HttpClient client)
        {
            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/accounts/{accountId}/cards", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var createCardResponse = JsonSerializer.Deserialize<CreateCardResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return createCardResponse;
        }
    }
}

