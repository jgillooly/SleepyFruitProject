using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SleepyFruitProject.Data;
using SleepyFruitProject.Models;

namespace SleepyFruitProject.Controllers
{
    public class QuizController : Controller
    {
        public UserDal dal;

        private static List<Question> questions = new List<Question>()
        {
            new Question(1, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
            new Question(2, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
            new Question(3, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
            new Question(4, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
            new Question(5, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
            new Question(6, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
            new Question(24, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) })
        };

        private static int questionNum = 0;

        public QuizController(UserDal indal)
        {
            dal = indal;

        }


        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult QuestionPage()
        {
            questionNum = dal.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier)).question;
            return View(questions[questionNum]);
        }

        [Authorize]
        [HttpPost]
        public IActionResult QuestionPage(bool correct)
        {
            if (correct)
            {
                questionNum++;
                User temp = dal.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
                temp.question = questionNum;
                dal.UpdateUser(temp);

                if (questionNum == questions.Count)
                {
                    return RedirectToAction("End_1", "Home");
                }

                return View(questions[questionNum]);
            }
            else
            {
                return View(questions[questionNum]);
            }
        }
    }
}
