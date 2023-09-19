using FintechChallenge.Helpers;
using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface IGetPeopleCardsService
{
    Task<PaginatedResult<CreateCardResponse>> GetPeopleCards(Guid peopleId, int take, int skip);
}