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
        [HttpGet]
        public IActionResult End_1()
        { 
            return View();
        }
        [HttpPost]
        public IActionResult End_1(FinalGuess fg)
        {
            //Custom validation
            if (fg.Answer1 != 2)
            {
                ModelState.AddModelError("Answer1", "What do you take me for an idiot?");
            }
            if (!fg.Answer2.Equals("Pineapple", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Answer2", "Your time thanks you for the gains");
            }
            if (!fg.Answer3.Equals("declaration"))
            {
                ModelState.AddModelError("Answer3", "I can't belive you didn't get this 1");
            }
            if (!fg.Answer4.Equals("pie", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("Answer4", "You deserve a worse time >:)");
            }
            if (!fg.Answer5.Equals("U"))
            {
                ModelState.AddModelError("Answer5", "C'mon the quiz isn't impossible!");
            }
            if (ModelState.IsValid)
            {
                //dal.AddMovie(m);
                return RedirectToAction("GloatScreen", "Home");
                //return View();
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
		public IActionResult FinishLine()
		{
			return View();
		}
        public IActionResult GloatScreen()
        {
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
