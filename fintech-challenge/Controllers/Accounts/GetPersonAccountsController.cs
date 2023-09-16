using FintechChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace FintechChallenge.Controllers;

[ApiController]
[Route("people/{peopleId:guid}/accounts")]
public class GetPersonAccountsController : ControllerBase
{
    private readonly IGetPersonAccountsService _getPersonAccountsService;

    public GetPersonAccountsController(IGetPersonAccountsService getPersonAccountsService)
    {
        _getPersonAccountsService = getPersonAccountsService;
    }

    [HttpGet()]
    public async Task<IActionResult> CreateAccount(Guid peopleId)
    {
        var personAccounts = await _getPersonAccountsService.GetAccountsByPeopleId(peopleId);


        return Ok(personAccounts);
    }
}