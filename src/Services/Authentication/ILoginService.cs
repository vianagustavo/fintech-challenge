

using FintechChallenge.Models;

namespace FintechChallenge.Services;

public interface ILoginService
{
    Task<string?> ValidatePeople(PeopleLoginRequest loginRequest);
}