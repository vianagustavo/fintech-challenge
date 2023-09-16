using FintechChallenge.Database;
using FintechChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace FintechChallenge.Repositories;

public class CardsRepository : ICardsRepository
{
    private readonly DatabaseContext _context;

    public CardsRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task CreateCard(Card card)
    {
        await _context.Cards.AddAsync(card);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Card>> GetCardsByAccountId(Guid accountId)
    {
        var accountCards = await _context.Cards
        .Where(card => card.AccountId == accountId)
        .ToListAsync();

        return accountCards;
    }

    public async Task<List<Card>> GetCardsByPeopleId(Guid peopleId)
    {
        var cards = await _context.Cards
            .Where(card => card.Account.PeopleId == peopleId)
            .ToListAsync();

        return cards;
    }
}
