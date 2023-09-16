using FintechChallenge.Models;

namespace FintechChallenge.Services;

public interface ITokenService
{
    string GenerateToken(People people);
}