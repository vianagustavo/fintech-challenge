using FintechChallenge.Domain;
using FintechChallenge.Exceptions;
using FintechChallenge.Helpers;
using FintechChallenge.Models;

namespace FintechChallenge.Services
{
    public class GetAccountTransactionsService : IGetAccountTransactionsService
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly ITransactionsRepository _transactionRepository;

        public GetAccountTransactionsService(IAccountsRepository accountsRepository, ITransactionsRepository transactionsRepository)
        {
            _accountsRepository = accountsRepository;
            _transactionRepository = transactionsRepository;
        }

        public async Task<PaginatedResult<CreateTransactionResponse>> GetAccountTransactions(Guid accountId, int take, int skip, DateTime? startDate, DateTime? endDate)
        {
            var account = await _accountsRepository.GetAccountById(accountId);

            if (account == null)
            {
                throw new NotFoundException("Account not found");
            }

            var accountTransactions = await _transactionRepository.GetTransactionsByAccountId(accountId);

            var filteredAccountTransactions = accountTransactions = accountTransactions.Where(transaction =>
            transaction.CreatedAt >= startDate && transaction.CreatedAt <= endDate
            ).ToList();


            var formattedTransactions = filteredAccountTransactions.Select(transaction => new CreateTransactionResponse(
                Id: transaction.Id,
                Value: transaction.Value,
                Description: transaction.Description,
                CreatedAt: transaction.CreatedAt,
                UpdatedAt: transaction.UpdatedAt
                )).ToList();

            var paginationHelper = new PaginationHelper<CreateTransactionResponse>();
            var pagedResult = paginationHelper.Paginate(formattedTransactions, "transactions", take, skip);

            return pagedResult;
        }
    }
}