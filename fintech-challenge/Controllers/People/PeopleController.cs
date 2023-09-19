using FintechChallenge.Domain;
using FintechChallenge.Models;
using Microsoft.AspNetCore.Mvc;

namespace FintechChallenge.Controllers;

[ApiController]
[Route("people")]
public class PeopleController : ControllerBase
{
    private readonly ICreatePeopleService _createPeopleService;

    public PeopleController(ICreatePeopleService createPeopleService)
    {
        _createPeopleService = createPeopleService;
    }

    [HttpPost()]
    public async Task<IActionResult> CreatePeople(CreatePeopleRequest request)
    {
        var createPeopleResponse = await _createPeopleService.CreatePeople(request);


        return Created("", createPeopleResponse);
    }
}