using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using FintechChallenge.Models;
using FintechChallenge.Tests.Helpers;
using Xunit;

namespace FintechChallenge.Tests.Integration
{
    public class GetAccountCardsControllerIntegrationTests : IClassFixture<IntegrationsTestFactory>
    {
        private readonly HttpClient _client;

        public GetAccountCardsControllerIntegrationTests(IntegrationsTestFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAccountCards_ValidRequest_ReturnsOkWithAccounts()
        {
            var createPeopleRequest = new CreatePeopleRequest("Test Name", "12345678901", "1234");

            var mockedUserFactory = new CreatePeopleMockFactory();
            var peopleResponseObject = await mockedUserFactory.MockPeople(createPeopleRequest, _client);

            var validLoginRequest = new PeopleLoginRequest("12345678901", "1234");

            var mockedLoginFactory = new LoginPeopleMockFactory();
            var loginResponseObject = await mockedLoginFactory.MockLogin(validLoginRequest, _client);

            var createAccountRequest = new CreateAccountRequest("123", "12345678901");
            var mockedCreateAccountFactory = new CreateAccountMockFactory();
            var mockedAccount = await mockedCreateAccountFactory.MockCreateAccount(createAccountRequest, peopleResponseObject.Id, loginResponseObject.Token, _client);

            var createCardRequest = new CreateCardRequest("virtual", "5179 7447 8594 5000", "123");
            var mockedCreateCardFactory = new CreateCardMockFactory();
            var mockedCard = await mockedCreateCardFactory.MockCreateCard(createCardRequest, mockedAccount.Id, _client);

            var response = await _client.GetAsync($"/accounts/{mockedAccount.Id}/cards");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<GetAccountCardsResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.NotNull(responseObject);
        }
    }
}