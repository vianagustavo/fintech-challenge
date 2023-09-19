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
    public class CreateCardControllerIntegrationTests : IClassFixture<IntegrationsTestFactory>
    {
        private readonly HttpClient _client;

        public CreateCardControllerIntegrationTests(IntegrationsTestFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateCard_ValidRequest_ReturnsCreated()
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

            var createCardRequest = new CreateCardRequest("virtual", "5179 7447 8594 5000", "123");
            var mockedCreateCardFactory = new CreateCardMockFactory();
            var mockedCard = await mockedCreateCardFactory.MockCreateCard(createCardRequest, accountResponseObject.Id, _client);

            Assert.NotNull(mockedCard);
            Assert.Equal("123", mockedCard.Cvv);
        }
    }
}