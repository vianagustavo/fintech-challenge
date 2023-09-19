using FintechChallenge.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FintechChallenge.Controllers;

[ApiController]
[Route("accounts/{accountId:guid}/cards")]
[Authorize(Roles = "people")]
public class GetAccountCardsController : ControllerBase
{
    private readonly IGetAccountCardsService _getAccountCardsService;

    public GetAccountCardsController(IGetAccountCardsService getAccountCardsService)
    {
        _getAccountCardsService = getAccountCardsService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAccountCards(Guid accountId)
    {
        var accountCards = await _getAccountCardsService.GetAccountCards(accountId);


        return Ok(accountCards);
    }
}