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
            var createRequest = new CreatePeopleRequest("Test Name", "12345678901", "1234");
            var mockedUserFactory = new CreatePeopleMockFactory();
            var mockedUser = await mockedUserFactory.MockPeople(createRequest, _client);

            Assert.NotNull(mockedUser);
            Assert.Equal("Test Name", mockedUser.Name);
        }
    }
}