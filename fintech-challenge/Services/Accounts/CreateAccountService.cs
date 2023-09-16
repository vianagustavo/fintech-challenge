using FintechChallenge.Exceptions;
using FintechChallenge.Models;
using FintechChallenge.Repositories;

namespace FintechChallenge.Services;

public class CreateAccountService : ICreateAccountService
{
    private readonly IPeopleRepository _peopleRepository;
    private readonly IAccountsRepository _accountRepository;

    public CreateAccountService(IAccountsRepository accountRepository, IPeopleRepository peopleRepository)
    {
        _accountRepository = accountRepository;
        _peopleRepository = peopleRepository;
    }

    public async Task<CreateAccountResponse> CreateAccount(Guid peopleId, CreateAccountRequest createAccountRequest)
    {
        var existingPeople = await _peopleRepository.GetPeopleById(peopleId);

        if (existingPeople == null)
        {
            throw new NotFoundException("User not found");
        }

        var accountToBeCreated = new Account(
          Guid.NewGuid(),
          createAccountRequest.Branch,
          createAccountRequest.Account,
          0,
          peopleId,
          DateTime.UtcNow,
          DateTime.UtcNow);

        await _accountRepository.CreateAccount(accountToBeCreated);

        var response = new CreateAccountResponse(
            accountToBeCreated.Id,
            accountToBeCreated.AccountBranch,
            accountToBeCreated.AccountNumber,
            accountToBeCreated.CreatedAt,
            accountToBeCreated.UpdatedAt
        );

        return response;
    }
}
