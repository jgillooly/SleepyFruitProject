using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SleepyFruitProject.Data;
using SleepyFruitProject.Models;

namespace SleepyFruitProject.Controllers
{
	public class QuizController : Controller {
		private static List<Question> questions = new List<Question>() 
		{ 
			new Question(1, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
			new Question(2, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
			new Question(3, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
			new Question(4, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
			new Question(5, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
			new Question(6, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
			new Question(7, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Test Question",new List<Answer>() {new Answer("One", false), new Answer("Two", true), new Answer ("Three", false), new Answer("Four", false) }),
			//Question 9-16 need to be made with the final 5 questions containing the "final answer" which will be imput on the final page
			//the answers in order are 2, pineapple, declaration, pie, U
			new Question(9, "https://www.thecoast.net.nz/media/9714550/life-the-universe.png?rmode=crop&rnd=132627101774130000&height=395&width=635&quality=95&scale=both", "What is the answer to life, the universe, and everything?",new List<Answer>() {new Answer("42", true), new Answer("24", false), new Answer("The question", false), new Answer("The answer", false) }),
			new Question(10, "https://media2.giphy.com/media/l2JdTa0yVuHBpzIE8/200w.gif?cid=6c09b952h42dt7abhbyaoo40rixk6885hhlz64yfn8s4e8f3&ep=v1_gifs_search&rid=200w.gif&ct=g", "What is the best fruit?",new List<Answer>() {new Answer("Strawberry", false), new Answer("Lychee", false), new Answer("Dragonfruit", false), new Answer("Grape", true) }),
			new Question(11, "https://media0.giphy.com/media/3o72F4nTnhd0fxsVhK/giphy.gif", "If a pineapple and a coconut had a dance-off, which fruit would be declared the champion of the tropical dance floor?",new List<Answer>() {new Answer("Watermelon", false), new Answer("Both! Yay Peace!", false), new Answer("The Child", true), new Answer("Avocado", false) }),

			//Final 5 questions
			new Question(12, "https://w7.pngwing.com/pngs/851/880/png-transparent-banana-word-image.png", "How many letters are there if you remove the 'b' an 'n'?",new List<Answer>() {new Answer("Enough?", false), new Answer("The peel", false), new Answer("2", true), new Answer("4", true) }),
			new Question(13, "https://static.vecteezy.com/system/resources/previews/008/008/273/non_2x/abstract-red-strawberry-with-crown-logo-design-graphic-symbol-icon-illustration-creative-idea-vector.jpg", "Which fruits crown makes the other fruits jealous?",new List<Answer>() {new Answer("Pineapple", true), new Answer("Orange", false), new Answer("Pear", false), new Answer("Pomegranate", false) }),
			new Question(14, "https://media.defense.gov/2009/Jun/30/2000533726/2000/2000/0/040929-F-5102W-001.JPG", "The fruits want rights and they are following our ancestors!! Quick sign the right document!",new List<Answer>() {new Answer("fruit freedom", false), new Answer("declaration", true), new Answer("rights", false), new Answer("bills (ew)", false) }),
			new Question(15, "https://img.jagranjosh.com/images/2023/October/16102023/Find-the-missing-value-of-fruits.jpg", "You should know the answer.",new List<Answer>() {new Answer("3.14", false), new Answer("pie", true), new Answer("π", false), new Answer("fruitcake", false) }),
			new Question(16, "https://madnews.wordpress.com/files/2008/01/soulja-boy.jpg", "Now watch me ________! Crank Dat...",new List<Answer>() {new Answer("U", true), new Answer("yo", false), new Answer("uhhhh", false), new Answer("Never heard that song.", false) })
		};



        public UserDal dal;
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
