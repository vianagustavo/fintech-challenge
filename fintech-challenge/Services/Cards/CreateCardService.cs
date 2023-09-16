using FintechChallenge.Exceptions;
using FintechChallenge.Models;
using FintechChallenge.Repositories;

namespace FintechChallenge.Services;

public class CreateCardService : ICreateCardService
{
    private readonly ICardsRepository _cardsRepository;
    private readonly IAccountsRepository _accountsRepository;

    public CreateCardService(IAccountsRepository accountsRepository, ICardsRepository cardsRepository)
    {
        _accountsRepository = accountsRepository;
        _cardsRepository = cardsRepository;
    }

    public async Task<CreateCardResponse> CreateCard(Guid accountId, CreateCardRequest createCardRequest)
    {
        if (createCardRequest.Cvv.Length != 3)
        {
            throw new BadRequestException("CVV must have 3 digits");
        }

        var existingAccount = await _accountsRepository.GetAccountById(accountId);

        if (existingAccount == null)
        {
            throw new NotFoundException("Account not found");
        }

        var accountCards = await _cardsRepository.GetCardsByAccountId(accountId);

        bool hasPhysicalCard = accountCards.Any(card => card.Type == CreditCardType.Physical);

        var parsedCardType = ToCreditCardType(createCardRequest.Type);

        if (hasPhysicalCard && parsedCardType == CreditCardType.Physical)
        {
            throw new BadRequestException("Account already has a physical card");
        }

        var cardToBeCreated = new Card(
          Guid.NewGuid(),
          parsedCardType,
          createCardRequest.Number,
          createCardRequest.Cvv,
          accountId,
          DateTime.UtcNow,
          DateTime.UtcNow);

        await _cardsRepository.CreateCard(cardToBeCreated);

        string cardLastFourDigits = createCardRequest.Number.Substring(createCardRequest.Number.Length - 4);

        var response = new CreateCardResponse(
            cardToBeCreated.Id,
            createCardRequest.Type,
            cardLastFourDigits,
            cardToBeCreated.Cvv,
            cardToBeCreated.CreatedAt,
            cardToBeCreated.UpdatedAt
        );

        return response;
    }

    private static CreditCardType ToCreditCardType(string value)
    {
        if (Enum.TryParse(typeof(CreditCardType), value, ignoreCase: true, out var result))
        {
            if (result != null)
            {
                return (CreditCardType)result;
            }
        }
        throw new BadRequestException($"Invalid credit card type: {value}");
    }
}
