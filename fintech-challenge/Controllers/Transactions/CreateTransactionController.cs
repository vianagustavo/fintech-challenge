using FintechChallenge.Domain;
using FintechChallenge.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FintechChallenge.Controllers;

[ApiController]
[Route("accounts/{accountId:guid}/transactions")]
[Authorize(Roles = "people")]
public class CreateTransactionController : ControllerBase
{
    private readonly ICreateTransactionService _createTransactionService;

    public CreateTransactionController(ICreateTransactionService createTransactionService)
    {
        _createTransactionService = createTransactionService;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateTransaction(Guid accountId, CreateTransactionRequest request)
    {
        var createTransactionResponse = await _createTransactionService.CreateTransaction(accountId, request);


        return Created("", createTransactionResponse);
    }
}