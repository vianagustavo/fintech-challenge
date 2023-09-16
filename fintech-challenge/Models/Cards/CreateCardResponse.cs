namespace FintechChallenge.Models;

public record CreateCardResponse(
    Guid Id,
    string Type,
    string Number,
    string Cvv,
    DateTime CreatedAt,
    DateTime UpdatedAt
);