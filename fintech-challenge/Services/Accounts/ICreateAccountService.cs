using FintechChallenge.Models;

namespace FintechChallenge.Services;

public interface ICreateAccountService
{
    Task<CreateAccountResponse> CreateAccount(Guid peopleId, CreateAccountRequest createAccountRequest);
}