using FintechChallenge.Domain;
using FintechChallenge.Exceptions;
using FintechChallenge.Models;

namespace FintechChallenge.Services;

public class LoginService : ILoginService
{
    private readonly IPeopleRepository _peopleRepository;
    private readonly ITokenService _tokenService;

    public LoginService(IPeopleRepository peopleRepository, ITokenService tokenService)
    {
        _peopleRepository = peopleRepository;
        _tokenService = tokenService;
    }
    public async Task<PeopleLoginResponse?> ValidatePeople(PeopleLoginRequest loginRequest)
    {
        var people = await _peopleRepository.GetPeopleByDocument(loginRequest.Document);

        if (people == null)
        {
            throw new BadRequestException("Document/password are incorrect");
        }

        var passwordMatch = BCrypt.Net.BCrypt.Verify(loginRequest.Password, people.Password);

        if (passwordMatch)
        {
            var token = _tokenService.GenerateToken(people);

            var response = new PeopleLoginResponse(token);

            return response;
        }

        throw new BadRequestException("Document/password are incorrect");
    }
}