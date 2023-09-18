namespace FintechChallenge.Services;

public interface IGetAccountBalanceService
{
    Task<decimal> GetAccountBalance(Guid peopleId);
}