using FintechChallenge.Exceptions;
using FintechChallenge.Models;
using FintechChallenge.Repositories;

namespace FintechChallenge.Services;

public class CreateTransactionService : ICreateTransactionService
{
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly IAccountsRepository _accountsRepository;

    public CreateTransactionService(IAccountsRepository accountsRepository, ITransactionsRepository transactionsRepository)
    {
        _accountsRepository = accountsRepository;
        _transactionsRepository = transactionsRepository;
    }

    public async Task<CreateTransactionResponse> CreateTransaction(Guid accountId, CreateTransactionRequest createTransactionRequest)
    {
        var account = await _accountsRepository.GetAccountById(accountId);

        if (account == null)
        {
            throw new NotFoundException("Account not found");
        }

        if (account.AccountBalance + createTransactionRequest.Value < 0)
        {
            throw new BadRequestException("Invalid account balance");
        }

        await _accountsRepository.UpdateAccountBalance(account, createTransactionRequest.Value);

        var transactionToBeCreated = new Transaction(
          Guid.NewGuid(),
          createTransactionRequest.Value,
          createTransactionRequest.Description,
          accountId,
          null,
          DateTime.UtcNow,
          DateTime.UtcNow);

        await _transactionsRepository.CreateTransaction(transactionToBeCreated);

        var response = new CreateTransactionResponse(
            transactionToBeCreated.Id,
            transactionToBeCreated.Value,
            transactionToBeCreated.Description,
            transactionToBeCreated.CreatedAt,
            transactionToBeCreated.UpdatedAt
        );

        return response;
    }
}
