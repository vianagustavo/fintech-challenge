namespace FintechChallenge.Models;

public record CreateCardRequest(
    string Type,
    string Number,
    string Cvv
);