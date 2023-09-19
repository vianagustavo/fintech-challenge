namespace FintechChallenge.Domain;

public interface IGetAccountBalanceService
{
    Task<decimal> GetAccountBalance(Guid peopleId);
}