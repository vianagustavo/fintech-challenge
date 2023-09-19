using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using FintechChallenge.Models;
using FintechChallenge.Tests.Helpers;
using Xunit;

namespace FintechChallenge.Tests.Integration
{
    public class RevertTransactionControllerIntegrationTests : IClassFixture<IntegrationsTestFactory>
    {
        private readonly HttpClient _client;

        public RevertTransactionControllerIntegrationTests(IntegrationsTestFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task RevertTransactions_ValidRequest_ReturnsOk()
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

            var createTransactionRequest = new CreateTransactionRequest(999, "IPhone 30");
            var mockedTransactionCardFactory = new CreateTransactionMockFactory();
            var mockedTransaction = await mockedTransactionCardFactory.MockCreateTransaction(createTransactionRequest, mockedAccount.Id, _client);

            var content = new StringContent("", Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/accounts/{mockedAccount.Id}/transactions/{mockedTransaction.Id}/revert", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}