using FintechChallenge.Models;
using FintechChallenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace FintechChallenge.Controllers;

[ApiController]
[Route("people/login")]
public class PeopleLoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public PeopleLoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost()]
    public async Task<IActionResult> LoginPeople(PeopleLoginRequest request)
    {
        var token = await _loginService.ValidatePeople(request);

        return Ok(token);
    }
}