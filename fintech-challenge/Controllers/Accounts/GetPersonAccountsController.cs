using FintechChallenge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FintechChallenge.Controllers;

[ApiController]
[Route("people/{peopleId:guid}/accounts")]
[Authorize(Roles = "people")]
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