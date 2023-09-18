using FintechChallenge.Models;

namespace FintechChallenge.Repositories;

public interface IAccountsRepository
{
    Task CreateAccount(Account account);
    Task<Account?> GetAccountById(Guid id);
    Task<List<Account>> GetAccountsByPeopleId(Guid peopleId);
    Task UpdateAccountBalance(Account account, decimal valueToAdd);
}