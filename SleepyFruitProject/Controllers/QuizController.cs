using Microsoft.AspNetCore.Mvc;
using SleepyFruitProject.Models;

namespace SleepyFruitProject.Controllers 
{
	public class QuizController : Controller {
		private static List<Question> questions = new List<Question>() { 
			new Question(1, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Are Zebras black with white stripes or white with black stripes?",new List<Answer>() {new Answer("B&W", false), new Answer("W&B", false), new Answer ("Both", false), new Answer("Who Cares", true) }),
			new Question(2, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "What's 9 + 10?",new List<Answer>() {new Answer("19", true), new Answer("21", false), new Answer ("You Stupid", false), new Answer("I don't like this game", false) }),
			new Question(3, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "*coin flip* Call it...",new List<Answer>() {new Answer("Heads", false), new Answer("Tails", true), new Answer ("Neither", false), new Answer("Both", false) }),
			new Question(4, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Is the Lord of the Rings a good movie?",new List<Answer>() {new Answer("Yes", false), new Answer("No", false), new Answer ("It's trash", true), new Answer("*bleugh*", false) }),
			new Question(5, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "Have you showered today?",new List<Answer>() {new Answer("Yes", false), new Answer("No", false), new Answer ("I'm a Neumont Student", true), new Answer("*sniff sniff*", false) }),
			new Question(6, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "What is a grape?",new List<Answer>() {new Answer("Fruit", false), new Answer("Vegetable", false), new Answer ("Berry", true), new Answer("Smash", false) }),
			new Question(7, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "What is the answer to this question?",new List<Answer>() {new Answer("What", false), new Answer("IDK", false), new Answer ("Found", false), new Answer("Sleepy", true) }),
			new Question(8, "https://img.imageboss.me/fourwinds/width/425/dpr:2/shop/products/blackmonukka.jpg?v=1538780984", "What starts with e ends with e but only has one letter in it?",new List<Answer>() {new Answer("Enevelope", false), new Answer("E", true), new Answer ("Explore", false), new Answer("Exile", false) }),
		};
		private static int questionNum = 0;

		public IActionResult Index() 
		{
			return View();
		}

		public IActionResult QuestionPage(bool correct) 
		{
			
			if (correct)
			{
				questionNum++;

				if(questionNum == questions.Count)
				{
					return RedirectToAction("End_1", "Home");
				}

				return View(questions[questionNum]);
			} else
			{
				return View(questions[questionNum]);
			}
		}
	}
}
