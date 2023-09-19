using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface IRevertTransactionService
{
    Task<CreateTransactionResponse> RevertTransaction(Guid accountId, Guid transactionId);
}