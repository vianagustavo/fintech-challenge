namespace FintechChallenge.Models;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public string Description { get; set; }
    public Guid AccountId { get; set; }
    public Account Account { get; set; } = null!;
    public DateTime? RevertedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Transaction(
        Guid id,
        decimal value,
        string description,
        Guid accountId,
        DateTime? revertedAt,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id = id;
        Value = value;
        Description = description;
        AccountId = accountId;
        RevertedAt = revertedAt;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}