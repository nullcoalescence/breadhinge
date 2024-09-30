using breadhinge.Models;
using breadhinge.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace breadhinge.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SpotifyService _spotifyService;

        public HomeController(ILogger<HomeController> logger, SpotifyService spotifyService)
        {
            _logger = logger;
            _spotifyService = spotifyService;
        }

        public async Task<IActionResult> Index()
        {
            await _spotifyService.BuildSpotify();
            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
