using FintechChallenge.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FintechChallenge.Controllers;

[ApiController]
[Route("accounts/{accountId:guid}/balance")]
[Authorize(Roles = "people")]
public class GetAccountBalanceController : ControllerBase
{
    private readonly IGetAccountBalanceService _getAccountBalanceService;

    public GetAccountBalanceController(IGetAccountBalanceService getAccountBalanceService)
    {
        _getAccountBalanceService = getAccountBalanceService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAccountBalance(Guid accountId)
    {
        var balance = await _getAccountBalanceService.GetAccountBalance(accountId);


        return Ok(balance);
    }
}