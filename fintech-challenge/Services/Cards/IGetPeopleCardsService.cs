using FintechChallenge.Helpers;
using FintechChallenge.Models;

namespace FintechChallenge.Services;

public interface IGetPeopleCardsService
{
    Task<PaginatedResult<CreateCardResponse>> GetPeopleCards(Guid peopleId, int take, int skip);
}