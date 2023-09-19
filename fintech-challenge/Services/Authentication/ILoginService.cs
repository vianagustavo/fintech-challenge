

using FintechChallenge.Models;

namespace FintechChallenge.Services;

public interface ILoginService
{
    Task<PeopleLoginResponse?> ValidatePeople(PeopleLoginRequest loginRequest);
}