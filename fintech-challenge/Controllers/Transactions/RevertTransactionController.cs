using System.Globalization;
using FintechChallenge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FintechChallenge.Controllers;

[ApiController]
[Route("accounts/{accountId:guid}/transactions/{transactionId:guid}/revert")]
[Authorize(Roles = "people")]
public class RevertTransactionController : ControllerBase
{
    private readonly IRevertTransactionService _revertTransactionService;

    public RevertTransactionController(IRevertTransactionService revertTransactionService)
    {
        _revertTransactionService = revertTransactionService;
    }

    [HttpGet()]
    public async Task<IActionResult> RevertTransaction(Guid accountId, Guid transactionId)
    {
        var revertTransaction = await _revertTransactionService.RevertTransaction(accountId, transactionId);


        return Ok(revertTransaction);
    }
}