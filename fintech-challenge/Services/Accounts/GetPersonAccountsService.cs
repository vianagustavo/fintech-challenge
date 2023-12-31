using FintechChallenge.Domain;
using FintechChallenge.Models;

namespace FintechChallenge.Services;

public class GetPersonAccountsService : IGetPersonAccountsService
{
    private readonly IAccountsRepository _accountRepository;

    public GetPersonAccountsService(IAccountsRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<List<CreateAccountResponse>> GetPersonAccounts(Guid peopleId)
    {
        var personAccounts = await _accountRepository.GetAccountsByPeopleId(peopleId);

        var response = personAccounts.Select(account => new CreateAccountResponse(
            Id: account.Id,
            Branch: account.AccountBranch,
            Account: account.AccountNumber,
            CreatedAt: account.CreatedAt,
            UpdatedAt: account.UpdatedAt
        ))
        .ToList();

        return response;
    }
}
