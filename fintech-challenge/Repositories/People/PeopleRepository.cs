using FintechChallenge.Database;
using FintechChallenge.Domain;
using FintechChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace FintechChallenge.Repositories;

public class PeopleRepository : IPeopleRepository
{
    private readonly DatabaseContext _context;

    public PeopleRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task CreatePeople(People people)
    {
        await _context.People.AddAsync(people);

        await _context.SaveChangesAsync();
    }

    public async Task<People?> GetPeopleByDocument(string document)
    {
        var people = await _context.People.FirstOrDefaultAsync(person => person.Document == document);

        return people;
    }

    public async Task<People?> GetPeopleById(Guid id)
    {
        var people = await _context.People.FindAsync(id);

        return people;
    }
}
