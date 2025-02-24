using Microsoft.AspNetCore.Mvc;

namespace MaidanVault_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetHomeData(int userId)
        {
            var response = await _homeService.GetHomeDataAsync(userId);
            return Ok(new { message = "Home data retrieved successfully!", data = response });
        }
    }
}
