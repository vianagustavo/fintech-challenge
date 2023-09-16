namespace FintechChallenge.Models;

public record CreateAccountResponse(
    Guid Id,
    string Branch,
    string Account,
    DateTime CreatedAt,
    DateTime UpdatedAt
);