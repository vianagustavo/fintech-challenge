using FintechChallenge.Models;

namespace FintechChallenge.Repositories;

public interface ICardsRepository
{
    Task CreateCard(Card card);
    Task<List<Card>> GetCardsByAccountId(Guid accountId);
}