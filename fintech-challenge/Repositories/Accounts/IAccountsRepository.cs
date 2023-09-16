using FintechChallenge.Models;

namespace FintechChallenge.Repositories;

public interface IAccountsRepository
{
    Task CreateAccount(Account account);
    Task<List<Account>> GetAccountsByPeopleId(Guid peopleId);
}