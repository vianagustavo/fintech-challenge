namespace FintechChallenge.Models;

public record CreateTransactionResponse(
    Guid Id,
    decimal Value,
    string Description,
    DateTime CreatedAt,
    DateTime UpdatedAt
);