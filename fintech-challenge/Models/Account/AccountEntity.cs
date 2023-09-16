namespace FintechChallenge.Models;

public class Account
{
    public Guid Id { get; set; }
    public string AccountBranch { get; set; }
    public string AccountNumber { get; set; }
    public decimal AccountBalance { get; set; }
    public Guid PeopleId { get; set; }
    public People People { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<Card> Cards { get; } = new List<Card>();

    public Account(
        Guid id,
        string accountBranch,
        string accountNumber,
        decimal accountBalance,
        Guid peopleId,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id = id;
        AccountBranch = accountBranch;
        AccountNumber = accountNumber;
        AccountBalance = accountBalance;
        PeopleId = peopleId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}