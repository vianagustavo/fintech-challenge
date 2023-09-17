using FintechChallenge.Models;

namespace FintechChallenge.Services;

public interface ICreateTransactionService
{
    Task<CreateTransactionResponse> CreateTransaction(Guid accountId, CreateTransactionRequest createAccountRequest);
}