using FintechChallenge.Domain;
using FintechChallenge.Exceptions;
using FintechChallenge.Models;

namespace FintechChallenge.Services;

public class GetAccountCardsService : IGetAccountCardsService
{
    private readonly IAccountsRepository _accountsRepository;
    private readonly ICardsRepository _cardsRepository;

    public GetAccountCardsService(IAccountsRepository accountsRepository, ICardsRepository cardsRepository)
    {
        _accountsRepository = accountsRepository;
        _cardsRepository = cardsRepository;
    }

    public async Task<GetAccountCardsResponse> GetAccountCards(Guid accountId)
    {
        var account = await _accountsRepository.GetAccountById(accountId);

        if (account == null)
        {
            throw new NotFoundException("Account not found");
        }

        var accountCards = await _cardsRepository.GetCardsByAccountId(accountId);

        var formattedCards = accountCards.Select(card => new CreateCardResponse(
        Id: card.Id,
        Type: card.Type.ToString().ToLower(),
        Number: card.CardNumber.Substring(card.CardNumber.Length - 4),
        Cvv: card.Cvv,
        CreatedAt: card.CreatedAt,
        UpdatedAt: card.UpdatedAt
        )).ToList();

        var response = new GetAccountCardsResponse(
            account.Id,
            account.AccountBranch,
            account.AccountNumber,
            formattedCards,
            account.CreatedAt,
            account.UpdatedAt
        );

        return response;
    }
}
