using Base.API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Base.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var token = AuthExtension.GenerateJwtToken("murilobdo", "DHASNVGFGDKLFNGDKFHBHJDSDHASNVGFGDKLFNGDKFHBHJDS");
        return Ok(token);
    }
}