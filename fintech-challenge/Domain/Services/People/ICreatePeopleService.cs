using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface ICreatePeopleService
{
    Task<CreatePeopleResponse> CreatePeople(CreatePeopleRequest createPeopleRequest);
}