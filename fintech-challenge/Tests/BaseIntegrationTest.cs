using MediatR;
using Xunit;

namespace FintechChallenge.Tests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationsTestFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;

    protected BaseIntegrationTest(IntegrationsTestFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
    }
}