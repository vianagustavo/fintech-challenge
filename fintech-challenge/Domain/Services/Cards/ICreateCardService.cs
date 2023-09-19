using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface ICreateCardService
{
    Task<CreateCardResponse> CreateCard(Guid accountId, CreateCardRequest createCardRequest);
}