namespace FintechChallenge.Models;

public record CreatePeopleRequest(
    string Name,
    string Document,
    string Password
);