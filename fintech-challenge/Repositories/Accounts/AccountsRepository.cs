using FintechChallenge.Database;
using FintechChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace FintechChallenge.Repositories;

public class AccountsRepository : IAccountsRepository
{
    private readonly DatabaseContext _context;

    public AccountsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task CreateAccount(Account account)
    {
        await _context.Accounts.AddAsync(account);

        await _context.SaveChangesAsync();
    }
    public async Task<Account?> GetAccountById(Guid id)
    {
        var account = await _context.Accounts.FindAsync(id);

        return account;
    }

    public async Task<List<Account>> GetAccountsByPeopleId(Guid peopleId)
    {
        var personAccounts = await _context.Accounts
                .Where(account => account.PeopleId == peopleId)
                .ToListAsync();

        return personAccounts;
    }
}
