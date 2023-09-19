using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface ICreateAccountService
{
    Task<CreateAccountResponse> CreateAccount(Guid peopleId, CreateAccountRequest createAccountRequest);
}