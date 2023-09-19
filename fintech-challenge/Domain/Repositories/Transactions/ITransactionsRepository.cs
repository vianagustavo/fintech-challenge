using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface ITransactionsRepository
{
    Task CreateTransaction(Transaction transaction);
    Task<Transaction?> GetTransactionById(Guid id);
    Task<List<Transaction>> GetTransactionsByAccountId(Guid accountId);
    Task UpdateRevertedTransaction(Transaction transaction);
}