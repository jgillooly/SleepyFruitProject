using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SleepyFruitProject.Data;
using SleepyFruitProject.Models;

namespace SleepyFruitProject.Controllers {
	public class QuizController : Controller {
		Random Random = new Random();

		private static List<Question> questions = new List<Question>() { 
			new Question(1, "https://lasercraftum.com/cdn/shop/products/marty-the-zebra-madagascar-layered-design-for-cutting-246_1200x1200.jpg?v=1675266851", "Are Zebras black with white stripes or white with black stripes?",new List<Answer>() {new Answer("B&W", false), new Answer("W&B", false), new Answer ("Both", false), new Answer("Who Cares", true) }),
			new Question(2, "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBw8QEA8NDw8NDQ8NDQ0NDQ0NDQ8NDQ0NFREWFhURFRUY" +
				"HSggGBolGxUVITEhJSkrLi4uFx8zODMsNygtLisBCgoKDg0OFxAQFy0dFR0rLS0rLS0tKy0tLSsrLS0rLSstKystLS04LSsrKy0tLS0rKy0rKysrLS0tKz" +
				"crLS0tLf/AABEIAK8BIQMBIgACEQEDEQH/xAAbAAACAwEBAQAAAAAAAAAAAAABBAACAwUGB//EADYQAAICAQIDBQYFAgcAAAAAAAABAgMRBCEFEjETIlFhcQYy" +
				"QYGRoQcUI1KxQmIkM2Nyc4LB/8QAGgEAAgMBAQAAAAAAAAAAAAAAAQMAAgQFBv/EACQRAQACAgICAgMBAQEAAAAAAAABAgMRITEEEiIyE0FhUSMF/9oADAMBAAIRAxEAP" +
				"wD5eQgMG5nEmABAIYCQgdIgAlWSUTIcACiAjM2aGVhUYhnbZhCFjyzScsszYi9tnRCmCYNIosigscGsC2CJAQQBwDDDpEBkLRXAUWTGaZCyL1vdFbQtDt6WJtKJTh28Rm6OzfkYrT" +
				"8tHx0yjXkPZ4F+Gt4l6jEkSZGFXIzlI05SkokRiyrRq4lcF4lVkwYNJIq0XhVngmAtEii2wmA5SGnKEgaZkDgmDpMYEIQibQAURoKAEAQIGCBBkCIK6qWBnInrCtulq9lirCiIzSctFBwTI" +
				"SIAUQvGBETAeUvGJflGRAbYqJOQY5A9mHQbKuBRDcoGEkVmFol3OCbprwH9TDuP0EvZvfmXodXXxxXL0Zy8ttZdNVI+LkcKjtL1HJQK8Hr/AE8+LY46wWvyvFSTgVcBx1AlWyRZJqQcSjiOSqMbI" +
				"DKypMFpIo0bSRSSGbVmGEiRLNEUS0SqIC/KQntA6lkDJMgOqwITBA5AiIjIgNhCEAQIBAhCZJpFWKawbYpqyl+lq9lkHlIkWMxwYLJACgIKRpEoi6LQjRG0ImUUN1RHVVlXlA4jKggOCG+qmyjiL2IdsiLTQq8aXh2fZGOZzXhFP7nd4zVimb8jmewFPNfZH/TT+56b2s06hpbPWK+55/yb68mKujirvHtwuB0PsY+eX9x78sO8E0v6FS/tyOPTGbLm+cnUx8ON+XJLT+R13pysqSv5pM/G4VumErqT0N1ZzNRE04sszJ" +
				"V6OLOJnKOw3bExUdmbq22yzBRoMUWaAv8A0uprkz2ARzsH4ogj8kH+rgACQ7rkoiEIREIEIUDBUuVIkBIqWYEBAYnqxwX1cdil+l69liACZTUDEAUgoujRGaZFMMAYgO1dDmwmdnR1Zjk0YuVLM+cDlnYtqmoC0NXHI2ZiO1Gz08uuGKWwwdaOri4+py9S89CuSsa3C1Zl6r8Mqs32vwgv5PR+3u2lfnOC+4j+GtMK6rLpuEXNrDbSeBr8Q5p0VRTT574JY6YPJeRPt5rt4Y1hO8JqxRV/xx/g3nA00kcQgvCEV9i0kYbW+Un1jiC" +
				"riZWRG5RMZoMSOnOvicrUxOxqDlak1YeysnTk3IxS7svQZtMLF3X8jpUY7Emg1rdeqI0XqW69UP8A0VHb0n5Ygxv4shz9tenglJEyJKx5G4npIttxpjS4JEaIkXgERZAIEBZUICAhCEwDQsbbcC9t2UHWCuRF7T0bWFwZA2FISuOQqZUKQUdK3hNsdOtW+Xs5SUdpLmT80c5Fu0ljly+XrjO2SoZlFodTt0anEEvI4kFuOJ7DMV/XlW0BrrclOF1QsthXZZ2UJSSlZjPKvQrYskorWVkt7btuQ1qHd47odPQ1Ci13eMvgchs6eoug64willdWcyaGZdfpWv8AW7tsUEueS" +
				"iuiTwkM1cSstjRRKTkoXKSy8vqJ3z/TN+D6OXa6dvZWWRcfRMweTip6+2uWvDe2++H12vovRAYV0KOR5H9uvXoJowsNJzMbJl4GZJ6g5OpOrqJHJ1DNeHsnJPDm2mNvus2tF7XsdKjJYqaUrdeqKG+lXej/ALkNnqS69vVchDTtPIhzdtnD5dZHcagLV94aienpDi2WIQAxUUFgCGA2ACwCAiAEBBLayGVkQbOtOOUzlWww2Z8scm0lC5VBQgwSIiCEEYIoIYkRaGwxCRhFDdWlm1lJ4GUgJR153KqIzCDWzTRny5GTVXaQBOJvCllLFgMxOkL1VubjWus5JL6nttXo+y1PDqUv" +
				"crTl65ON7HaDttVB4zGt87+R6bi+/FKF+2nP8nI83N/0jH/Jlu8enx29G2ZzYZMymzz0OltSUjKyQZMxsYyIV2wvZy9S+p0Lmc281YYKvPBG0ws6G9phYdGjLYuMaKLc4pfuQux3hK/Vh6l7/WVadvUYATmIc3lrfNaoYLkCj1UOHKwGQjLQAhRUKCA5BkJME0iAYQMiAL6qvbIwRopaNwtWdS5LQYsa1FaSyKpGa1dHRO1iEIVEYlihMhiUbVdTow1klHljt5nMizatsbS2lZMTum9m8l9Osv1BGGR7TVYwx9a7kuZ0Y5EoicaHZZGqKzKclGK82NWT2FNHqez1Onn+26GfTOC3kW9aTMJjjc" +
				"8vpHszwL8pB8zTsn7zXReRyrZJ8Vbf9NCW/jg9enlZ8dzm36Orndjiud7OXxPER5M2y2vfueHdikVrEQE7V4ows1EfFElVX4Ixm6l8Yr6C4rC0ypPVQ8TCeqXhJ/I0dtXjD7GNuqrX9Ufk0OrX+BstfqdujEJ3Z+A1qNXB/FHOt1MTbioTeWc3lmVrI9Qs4KzZqrGmeZZ4HeE/5kfmI5HuErvp+Qcn1lKdvQ8xDPmIc/bW8CEqix6mHDFMJVFkXBApESDgiAQhGFEJgAQIGCrZcq0RC+p6CjQ5qegu47CLwZWWLkEE47kQnS6xCIiZIFpBHS0tNbXeYhAa09bk1FfEdj7Vl0HKuPTcvVqk9iuo4e4e81" +
				"0zgxUUuhsjgmYaWz6nNTcrEo5bUk0lu8pnQlTNwnNRbjBZk/BHZ/DPTxnddZJKXLGPLlJ4bZz/ADs9a0mWjBTdofQNHY3VW3nLhHOevQ8nw+UtRqtTCU5qNcu6k+m57G3ozxvsnvdq5+NmPueUwamMlnXtuNQ6dnCYfum/+xk+GVeDfq2dWwwkUjJb/R050uGVfs+7MZ8PqX9C+rOnIWtGVyW/0JhzbNPBdIr6Cl0I+C+h0LxC82YrTJViUorwX0MZm8uphYbKs9mLOhwhd5+hz2jo8J95+gcn1SnbqkK5IYtNbw5ZALI9NDhoFEIXVEsULIKAQgSIGCMIGRAIyEIhfUmJtqOqMpCLmVPz00PyXa8vfWq5eb+z" +
				"k6fU5CO3O2P5Fwz3nepJeRxEJkwSIAE9wRAmamPaaTTyuohSOUsfi4UsdnNvq2zKUt16hyY2D8lvjwXHb3L4YpaRQj3XdHDZ0fZPglekjLlbcp45mxPhOthZVTGMk2o4kvimeg0u2TxnlZcnyrP7dnFWNQ21Mu7L0f8AB5H2NW2ol43M9TrJYhN+EJP7HmPYtfo2P910mKwcYrm2+0O/IxmayM5MVC0sZitrGbugtaNqrJO5iN45eI2s24YJuVfUXte7GJ9RaZtqRLNs6PClu35HNZ0uF9GHJ0lO3QyQqQytDxyIRBwekcaVkQiC0WhUAoiQcBRUhCEQAkSIRBITJSdiRNjovqfeRhZLAbrcvJk9zNe0G1gXNtYy" +
				"8dcfAqwpEaFSuqjSCKYNa0SElrUh2mAvTEci1g044LtK/NhC0zeKyC6Gw23SsFqdTOuSnCTi087M9Vwr24axC+OcYXPHq/U8hMxwcryPEx5ftDVjzWq+sz4zRdTY4WRb7OW2VlbeBz/Y1f4fPjZP+T5uptdG1nrhjuh45qKFy1zxHOeVrKMNv/OmuOa0ntpjyYmeX1aTMZM8Np/bO1e/GMvNbM7eg9pabcRy4SfwfT6mC3h5aczB8Zqy7NgtYa9plZRjaxUQvMkrzn3MevkIWs34oIv2wfUXsGcbv0FrDXUmWJ1OGe6/U5mDp8Ofc+bBfpKdmiFchM5zyZAkaPSOOsiMCIGEHJOYBA7BAZDkWsvwCZ0OttpTwYz1KQt" +
				"OxsoJtlMijeWpZlK1soDAqbzK8V0JCBKChABDCChmqAqjaNmC8AbzgnOKu0z7Rl/fQadWuzBW685vbMDtYZyB6mJzMnIzUi6gLnkVWyrRr2YOQErbZotCRGiFfXYxLvcJ49OruzzOG2z3a9D01HEK7Y5g8+XxR89ya6bUyrfNFtMx5vDredx2dTPMcS9xcxGbKaLiCtjvtLG6+AZszRjms6k6bRPKucZ9BebNpC7HVLlRnS0DxD5s5rOjpPc+bJk6Wp2Y5yGZDOc//9k=", "What's 9 + 10?",new List<Answer>() {new Answer("19", true), new Answer("21", false), new Answer ("You Stupid", false), new Answer("I don't like this game", false) }),
			new Question(3, "https://watermark.lovepik.com/photo/20211202/large/lovepik-businessman-ready-to-toss-a-coin-picture_501448831.jpg", "*coin flip* Call it...",new List<Answer>() {new Answer("Heads", false), new Answer("Tails", true), new Answer ("Neither", false), new Answer("Both", false) }),
			new Question(4, "https://i.makeagif.com/media/5-07-2016/shULZy.gif", "Is the Lord of the Rings a good movie?",new List<Answer>() {new Answer("Yes", false), new Answer("No", false), new Answer ("It's trash", true), new Answer("*bleugh*", false) }),
			new Question(5, "https://i.makeagif.com/media/2-06-2022/f5-kPt.gif", "Have you showered today?",new List<Answer>() {new Answer("Yes", false), new Answer("No", false), new Answer ("I'm a Neumont Student", true), new Answer("*sniff sniff*", false) }),
			new Question(6, "https://i.makeagif.com/media/5-11-2015/6YJlfO.gif", "What is a grape?",new List<Answer>() {new Answer("Fruit", false), new Answer("Vegetable", false), new Answer ("Berry", false), new Answer("Smash", true) }),
			new Question(7, "https://i.imgflip.com/83eua0.jpg", "What is the answer to this question?",new List<Answer>() {new Answer("What", false), new Answer("IDK", false), new Answer ("Found", false), new Answer("Sleepy", true) }),
			new Question(8, "https://i.pinimg.com/originals/a5/5b/b1/a55bb1e92f75901273413b7549945d09.jpg", "What starts with e ends with e but only has one letter in it?",new List<Answer>() {new Answer("Enevelope", false), new Answer("E", true), new Answer ("Explore", false), new Answer("Exile", false) }),
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

		public IActionResult Index() 
		{
			return View();
		}

		//method for setting up other questions
		public void questionSetup() {
			//question2
			questions[2].Answers[0].correct = (Random.Next(2) == 0);
			questions[2].Answers[1].correct = !questions[2].Answers[0].correct;
		}
        public QuizController(UserDal indal)
        {
            dal = indal;

        }

        [Authorize]
        [HttpGet]
        public IActionResult QuestionPage() {
			questionSetup();
            questionNum = dal.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier)).question;
            return View(questions[questionNum]);
        }

        [Authorize]
        [HttpPost]
        public IActionResult QuestionPage(bool correct) {
            if (correct) {
                questionNum++;
                User temp = dal.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
                temp.question = questionNum;
                dal.UpdateUser(temp);

                if (questionNum == questions.Count) {
                    return RedirectToAction("End_1", "Home");
                }

				return View(questions[questionNum]);
			} else {
				return View(questions[questionNum]);
			}
		}
	}
}