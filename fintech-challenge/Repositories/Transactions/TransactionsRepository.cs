using FintechChallenge.Database;
using FintechChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace FintechChallenge.Repositories;

public class TransactionsRepository : ITransactionsRepository
{
    private readonly DatabaseContext _context;

    public TransactionsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task CreateTransaction(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Transaction>> GetTransactionsByAccountId(Guid accountId)
    {
        var accountTransactions = await _context.Transactions
                .Where(transaction => transaction.AccountId == accountId)
                .ToListAsync();

        return accountTransactions;
    }
}
