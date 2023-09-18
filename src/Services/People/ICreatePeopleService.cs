using FintechChallenge.Models;

namespace FintechChallenge.Services;

public interface ICreatePeopleService
{
    Task<CreatePeopleResponse> CreatePeople(CreatePeopleRequest createPeopleRequest);
}