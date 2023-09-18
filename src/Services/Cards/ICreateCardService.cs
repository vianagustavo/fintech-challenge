using FintechChallenge.Models;

namespace FintechChallenge.Services;

public interface ICreateCardService
{
    Task<CreateCardResponse> CreateCard(Guid accountId, CreateCardRequest createCardRequest);
}