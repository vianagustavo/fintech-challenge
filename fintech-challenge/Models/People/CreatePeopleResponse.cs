namespace FintechChallenge.Models;

public record CreatePeopleResponse(
    Guid Id,
    string Name,
    string Document,
    DateTime CreatedAt,
    DateTime UpdatedAt
);