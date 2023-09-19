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
    public class CreateAccountMockFactory
    {
        public async Task<CreateAccountResponse> MockCreateAccount(CreateAccountRequest request, Guid peopleId, string bearerToken, HttpClient client)
        {
            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");

            var response = await client.PostAsync($"/people/{peopleId}/accounts", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var createAccountResponse = JsonSerializer.Deserialize<CreateAccountResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return createAccountResponse;
        }
    }
}

