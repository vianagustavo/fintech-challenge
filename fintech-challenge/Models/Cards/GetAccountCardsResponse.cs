namespace FintechChallenge.Models;

public record GetAccountCardsResponse(
    Guid Id,
    string Branch,
    string Account,
    List<CreateCardResponse> Cards,
    DateTime CreatedAt,
    DateTime UpdatedAt
);