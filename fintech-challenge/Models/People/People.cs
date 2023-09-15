namespace FintechChallenge.Models;

public class People
{
    public Guid Id { get; set;}
    public string Name { get; set;}
    public string Password { get; set;}
    public string Document { get; set;}
    public DateTime CreatedAt { get; set;}
    public DateTime UpdatedAt { get; set;}

    public People(
        Guid id,
        string name,
        string password,
        string document,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id = id;
        Name = name;
        Password = password;
        Document = document;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}