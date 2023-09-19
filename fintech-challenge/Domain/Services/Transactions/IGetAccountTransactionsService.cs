using FintechChallenge.Helpers;
using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface IGetAccountTransactionsService
{
    Task<PaginatedResult<CreateTransactionResponse>> GetAccountTransactions(Guid accountId, int take, int skip, DateTime? startDate, DateTime? endDate);
}