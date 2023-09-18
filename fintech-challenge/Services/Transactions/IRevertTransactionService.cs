using FintechChallenge.Models;

namespace FintechChallenge.Services;

public interface IRevertTransactionService
{
    Task<CreateTransactionResponse> RevertTransaction(Guid accountId, Guid transactionId);
}