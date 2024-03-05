using Microsoft.AspNetCore.Mvc;
using SleepyFruitProject.Data;
using SleepyFruitProject.Models;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SleepyFruitProject.Controllers
{
    public class HomeController : Controller
    {
        public UserDal dal;

        private readonly ILogger<HomeController> _logger;
        static HttpClient client = new HttpClient();
        static string baseURL = "https://api.quotable.io/quotes/random?limit=2";

        private static string Author = "";
        private static string Quote = "";

        public HomeController(ILogger<HomeController> logger, UserDal indal)
        {
            _logger = logger;
            dal = indal;

        }

        public IActionResult Question1()
        {
            return View();
        }

        public IActionResult Index()
        {
            GenerateQuote();

            ViewBag.Quote = Quote;
            ViewBag.Author = Author;

            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            if (dal.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier)) == null)
            {
                string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                dal.AddUser(new User(User.FindFirstValue(ClaimTypes.NameIdentifier), User.FindFirstValue(ClaimTypes.Name), User.FindFirstValue(ClaimTypes.Email)));
                return View();
            }
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

        ////TODO:: actually log in
        //[HttpPost]
        //public IActionResult LogIn(string email, string password)
        //{ 
        //    return RedirectToAction("Index", "Home");
        //}


        //TODO:: go to quiz
        public IActionResult QuizStart()
        {
            return View();
        }
        //
        public IActionResult InfoQuiz()
        {
            return View();
        }
        public IActionResult FinishLine()
        {
            User temp = dal.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
            temp.end_time = DateTime.Now;
            temp.BestTime = temp.ElapsedTime;
            dal.UpdateUser(temp);
            return View(dal.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier)));
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

        private async void GenerateQuote()
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("https://api.quotable.io")
            };

            var response = await client.GetAsync("/quotes/random?limit=2");
            var content = await response.Content.ReadAsStringAsync();

            var output = content.Split("},");

            var index1 = output[0].IndexOf("content");
            var index10 = output[0].IndexOf("author");
            var quote = output[0].Substring(index1 + 10, index10 - index1 - 14);
            var index2 = output[1].IndexOf("author");
            var index20 = output[1].IndexOf("tags");
            var author = output[1].Substring(index2 + 9, index20 - index2 - 12);
            Author = author;
            Quote = quote;

            ViewBag.Quote = Quote;
            ViewBag.Author = Author;

        }
    }



}
