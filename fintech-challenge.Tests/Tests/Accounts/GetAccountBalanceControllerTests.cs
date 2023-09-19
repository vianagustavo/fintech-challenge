using System;
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
    public class GetAccountBalanceControllerIntegrationTests : IClassFixture<IntegrationsTestFactory>
    {
        private readonly HttpClient _client;

        public GetAccountBalanceControllerIntegrationTests(IntegrationsTestFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAccountBalance_ValidRequest_ReturnsOkWithBalance()
        {
            var createPeopleRequest = new CreatePeopleRequest("Test Name", "12345678901", "1234");
            var mockedUserFactory = new CreatePeopleMockFactory();
            var peopleResponseObject = await mockedUserFactory.MockPeople(createPeopleRequest, _client);

            var validLoginRequest = new PeopleLoginRequest("12345678901", "1234");
            var mockedLoginFactory = new LoginPeopleMockFactory();
            var loginResponseObject = await mockedLoginFactory.MockLogin(validLoginRequest, _client);

            var createAccountRequest = new CreateAccountRequest("123", "12345678901");
            var mockedCreateAccountFactory = new CreateAccountMockFactory();
            var accountResponseObject = await mockedCreateAccountFactory.MockCreateAccount(createAccountRequest, peopleResponseObject.Id, loginResponseObject.Token, _client);

            var response = await _client.GetAsync($"/accounts/{accountResponseObject.Id}/balance");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GetAccountBalanceResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(responseObject);
            Assert.True(responseObject.Balance == 0);
        }
    }
}