using FintechChallenge.Exceptions;
using FintechChallenge.Repositories;

namespace FintechChallenge.Services;

public class GetAccountBalanceService : IGetAccountBalanceService
{
    private readonly IAccountsRepository _accountRepository;

    public GetAccountBalanceService(IAccountsRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<decimal> GetAccountBalance(Guid accountId)
    {
        var account = await _accountRepository.GetAccountById(accountId);

        if (account == null)
        {
            throw new NotFoundException("Account not found");
        }

        var response = account.AccountBalance;

        return response;
    }
}
