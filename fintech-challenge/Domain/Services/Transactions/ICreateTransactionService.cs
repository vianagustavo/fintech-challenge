using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface ICreateTransactionService
{
    Task<CreateTransactionResponse> CreateTransaction(Guid accountId, CreateTransactionRequest createAccountRequest);
}