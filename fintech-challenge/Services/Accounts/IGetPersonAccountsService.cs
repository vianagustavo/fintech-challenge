using FintechChallenge.Models;

namespace FintechChallenge.Services;

public interface IGetPersonAccountsService
{
    Task<List<CreateAccountResponse>> GetPersonAccounts(Guid peopleId);
}