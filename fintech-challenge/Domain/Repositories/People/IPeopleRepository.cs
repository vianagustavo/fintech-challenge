using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface IPeopleRepository
{
    Task CreatePeople(People people);
    Task<People?> GetPeopleByDocument(string document);
    Task<People?> GetPeopleById(Guid id);
}