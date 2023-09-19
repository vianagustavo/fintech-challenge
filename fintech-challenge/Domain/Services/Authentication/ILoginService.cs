using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface ILoginService
{
    Task<PeopleLoginResponse?> ValidatePeople(PeopleLoginRequest loginRequest);
}