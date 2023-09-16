using FintechChallenge.Models;

namespace FintechChallenge.Repositories;

public interface IPeopleRepository
{
    Task CreatePeople(People people);
    Task<People?> GetPeopleByDocument(string document);
}