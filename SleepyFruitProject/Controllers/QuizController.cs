using Microsoft.AspNetCore.Mvc;

namespace SleepyFruitProject.Controllers {
	public class QuizController : Controller {
		public IActionResult Index() {
			return View();
		}

		public IActionResult Question2() {
			return View();
		}
	}
}
