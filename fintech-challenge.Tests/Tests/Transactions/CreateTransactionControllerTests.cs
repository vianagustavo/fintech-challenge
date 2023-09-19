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
    public class CreateTransactionControllerIntegrationTests : IClassFixture<IntegrationsTestFactory>
    {
        private readonly HttpClient _client;

        public CreateTransactionControllerIntegrationTests(IntegrationsTestFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateTransaction_ValidRequest_ReturnsCreated()
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

            var createTransactionRequest = new CreateTransactionRequest(999, "IPhone 30");
            var mockedTransactionCardFactory = new CreateTransactionMockFactory();
            var mockedTransaction = await mockedTransactionCardFactory.MockCreateTransaction(createTransactionRequest, accountResponseObject.Id, _client);

            Assert.NotNull(mockedTransaction);
            Assert.Equal("IPhone 30", mockedTransaction.Description);
        }
    }
}