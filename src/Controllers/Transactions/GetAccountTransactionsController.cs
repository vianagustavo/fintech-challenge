using System.Globalization;
using FintechChallenge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FintechChallenge.Controllers;

[ApiController]
[Route("accounts/{accountId:guid}/transactions")]
[Authorize(Roles = "people")]
public class GetAccountTransactionsController : ControllerBase
{
    private readonly IGetAccountTransactionsService _getAccountTransactionsService;

    public GetAccountTransactionsController(IGetAccountTransactionsService getAccountTransactionsService)
    {
        _getAccountTransactionsService = getAccountTransactionsService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetPeopleCards(Guid accountId,
            [FromQuery] int take = 5,
            [FromQuery] int skip = 0,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
    {
        var accountTransactions = await _getAccountTransactionsService.GetAccountTransactions(accountId, take, skip, startDate, endDate);


        return Ok(accountTransactions);
    }
}