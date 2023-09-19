using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface IGetPersonAccountsService
{
    Task<List<CreateAccountResponse>> GetPersonAccounts(Guid peopleId);
}