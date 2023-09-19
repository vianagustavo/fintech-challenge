using FintechChallenge.Models;

namespace FintechChallenge.Domain;

public interface ITokenService
{
    string GenerateToken(People people);
}