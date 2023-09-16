using FintechChallenge.Models;

namespace FintechChallenge.Services;

public interface IGetAccountCardsService
{
    Task<GetAccountCardsResponse> GetAccountCards(Guid accountId);
}