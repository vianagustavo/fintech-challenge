using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface IGetAccountBalanceService
{
    Task<GetAccountBalanceResponse> GetAccountBalance(Guid peopleId);
}