namespace FintechChallenge.Models;

public record CreateTransactionRequest(
    decimal Value,
    string Description
);