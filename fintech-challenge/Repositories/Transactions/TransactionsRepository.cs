using FintechChallenge.Database;
using FintechChallenge.Domain;
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
    public async Task<Transaction?> GetTransactionById(Guid id)
    {
        var transaction = await _context.Transactions.FindAsync(id);

        return transaction;
    }
    public async Task<List<Transaction>> GetTransactionsByAccountId(Guid accountId)
    {
        var accountTransactions = await _context.Transactions
                .Where(transaction => transaction.AccountId == accountId)
                .ToListAsync();

        return accountTransactions;
    }

    public async Task UpdateRevertedTransaction(Transaction transaction)
    {
        transaction.RevertedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }
}
