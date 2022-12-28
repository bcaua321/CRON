using Microsoft.AspNetCore.Mvc;

namespace CRON.Api.Controllers.V1;

[ApiController]
[Route("v{version:apiVersion}")]
[ApiVersion("1.0")]
public class IndexController : ControllerBase
{
    [HttpGet("/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Message()
    {
        return Ok("Fullstack Challenge 20201026");
    }
}