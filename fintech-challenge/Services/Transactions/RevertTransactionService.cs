using FintechChallenge.Exceptions;
using FintechChallenge.Models;
using FintechChallenge.Repositories;

namespace FintechChallenge.Services;

public class RevertTransactionService : IRevertTransactionService
{
    private readonly ITransactionsRepository _transactionsRepository;
    private readonly IAccountsRepository _accountsRepository;

    public RevertTransactionService(IAccountsRepository accountsRepository, ITransactionsRepository transactionsRepository)
    {
        _accountsRepository = accountsRepository;
        _transactionsRepository = transactionsRepository;
    }

    public async Task<CreateTransactionResponse> RevertTransaction(Guid accountId, Guid transactionId)
    {
        var account = await _accountsRepository.GetAccountById(accountId);

        if (account == null)
        {
            throw new NotFoundException("Account not found");
        }

        var transaction = await _transactionsRepository.GetTransactionById(transactionId);

        if (transaction == null)
        {
            throw new NotFoundException("Transaction not found");
        }

        var valueToRevert = -transaction.Value;

        if (account.AccountBalance + valueToRevert < 0)
        {
            throw new BadRequestException("Invalid account balance");
        }

        await _accountsRepository.UpdateAccountBalance(account, valueToRevert);
        await _transactionsRepository.UpdateRevertedTransaction(transaction);

        var transactionToBeCreated = new Transaction(
          Guid.NewGuid(),
          valueToRevert,
          "Estorno de cobrança indevida.",
          accountId,
          DateTime.UtcNow,
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
