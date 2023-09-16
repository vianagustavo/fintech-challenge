namespace FintechChallenge.Models;

public class Card
{
    public Guid Id { get; set; }
    public CreditCardType Type { get; set; }
    public string CardNumber { get; set; }
    public string Cvv { get; set; }
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Card(
        Guid id,
        CreditCardType type,
        string cardNumber,
        string cvv,
        Guid accountId,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id = id;
        Type = type;
        CardNumber = cardNumber;
        Cvv = cvv;
        AccountId = accountId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}