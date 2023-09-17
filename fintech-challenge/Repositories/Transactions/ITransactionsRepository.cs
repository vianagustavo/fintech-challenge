using FintechChallenge.Models;

namespace FintechChallenge.Repositories;

public interface ITransactionsRepository
{
    Task CreateTransaction(Transaction transaction);
    Task<List<Transaction>> GetTransactionsByAccountId(Guid accountId);
}