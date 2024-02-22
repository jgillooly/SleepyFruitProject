using Microsoft.AspNetCore.Mvc;
using SleepyFruitProject.Models;
using System.Diagnostics;

namespace SleepyFruitProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //TODO:: actually log in
        [HttpPost]
        public IActionResult LogIn(string email, string password)
        { 
            return RedirectToAction("Index", "Home");
        }

        //TODO:: go to quiz
        public IActionResult QuizStart() {
            return View();
        }

		public IActionResult yay()
		{
			return Redirect("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
