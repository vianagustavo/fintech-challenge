using FintechChallenge.Exceptions;
using FintechChallenge.Models;
using FintechChallenge.Repositories;

namespace FintechChallenge.Services;

public class CreatePeopleService : ICreatePeopleService
{
    private readonly IPeopleRepository _repository;

    public CreatePeopleService(IPeopleRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreatePeopleResponse> CreatePeople(CreatePeopleRequest createPeopleRequest)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt();
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(createPeopleRequest.Password, salt);
        string formattedDocument = createPeopleRequest.Document.Replace(".", "").Replace("-", "");

        var existingDocument = await _repository.GetPeopleByDocument(formattedDocument);

        if (existingDocument != null)
        {
            throw new BadRequestException("Document has already been registered");
        }



        var personToBeCreated = new People(
            Guid.NewGuid(),
            createPeopleRequest.Name,
            hashedPassword,
            formattedDocument,
            DateTime.UtcNow,
            DateTime.UtcNow);

        await _repository.CreatePeople(personToBeCreated);

        var response = new CreatePeopleResponse(
            Guid.NewGuid(),
            createPeopleRequest.Name,
            formattedDocument,
            DateTime.UtcNow,
            DateTime.UtcNow
        );

        return response;
    }
}