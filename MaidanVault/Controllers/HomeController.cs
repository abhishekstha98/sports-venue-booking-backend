using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

 // Protects all routes in this controller
[ApiController]
[Route("api/home")]
public class HomeController : ControllerBase
{
    private readonly IHomeService _homeService;

    public HomeController(IHomeService homeService)
    {
        _homeService = homeService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetHomeData(int userId)
    {
        try
        {
            var response = await _homeService.GetHomeDataAsync(userId);
            return Ok(new { message = "Home data retrieved successfully!", data = response });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
