using FintechChallenge.Models;
using FintechChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace FintechChallenge.Controllers;

[ApiController]
[Route("people/{peopleId:guid}/accounts")]
public class CreateAccountController : ControllerBase
{
    private readonly ICreateAccountService _createAccountService;

    public CreateAccountController(ICreateAccountService createAccountService)
    {
        _createAccountService = createAccountService;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateAccount(Guid peopleId, CreateAccountRequest request)
    {
        var createAccountResponse = await _createAccountService.CreateAccount(peopleId, request);


        return Created("", createAccountResponse);
    }
}