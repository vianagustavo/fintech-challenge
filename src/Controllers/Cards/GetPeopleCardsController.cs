using FintechChallenge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FintechChallenge.Controllers;

[ApiController]
[Route("people/{peopleId:guid}/cards")]
[Authorize(Roles = "people")]
public class GetPeopleCardsController : ControllerBase
{
    private readonly IGetPeopleCardsService _getPeopleCardsService;

    public GetPeopleCardsController(IGetPeopleCardsService getPeopleCardsService)
    {
        _getPeopleCardsService = getPeopleCardsService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetPeopleCards(Guid peopleId,
            [FromQuery] int take = 5,
            [FromQuery] int skip = 0)
    {
        var accountCards = await _getPeopleCardsService.GetPeopleCards(peopleId, take, skip);


        return Ok(accountCards);
    }
}