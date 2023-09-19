using FintechChallenge.Domain;
using FintechChallenge.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FintechChallenge.Controllers;

[ApiController]
[Route("accounts/{accountId:guid}/cards")]
[Authorize(Roles = "people")]
public class CreateCardController : ControllerBase
{
    private readonly ICreateCardService _createCardService;

    public CreateCardController(ICreateCardService createCardService)
    {
        _createCardService = createCardService;
    }

    [HttpPost()]
    public async Task<IActionResult> CreateAccount(Guid accountId, CreateCardRequest request)
    {
        var createCardResponse = await _createCardService.CreateCard(accountId, request);


        return Created("", createCardResponse);
    }
}