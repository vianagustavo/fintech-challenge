using FintechChallenge.Domain;
using FintechChallenge.Exceptions;
using FintechChallenge.Models;

namespace FintechChallenge.Services;

public class GetAccountBalanceService : IGetAccountBalanceService
{
    private readonly IAccountsRepository _accountRepository;

    public GetAccountBalanceService(IAccountsRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<GetAccountBalanceResponse> GetAccountBalance(Guid accountId)
    {
        var account = await _accountRepository.GetAccountById(accountId);

        if (account == null)
        {
            throw new NotFoundException("Account not found");
        }

        var response = new GetAccountBalanceResponse(account.AccountBalance);

        return response;
    }
}
