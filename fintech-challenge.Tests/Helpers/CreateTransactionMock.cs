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
    public class CreateTransactionMockFactory
    {
        public async Task<CreateTransactionResponse> MockCreateTransaction(CreateTransactionRequest request, Guid accountId, HttpClient client)
        {
            var jsonRequest = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/accounts/{accountId}/transactions", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var createTransactionResponse = JsonSerializer.Deserialize<CreateTransactionResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return createTransactionResponse;
        }
    }
}

