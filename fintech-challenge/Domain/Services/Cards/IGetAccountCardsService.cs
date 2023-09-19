using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface IGetAccountCardsService
{
    Task<GetAccountCardsResponse> GetAccountCards(Guid accountId);
}