using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SleepyFruitProject.Data;
using SleepyFruitProject.Models;

namespace SleepyFruitProject.Controllers
{
	public class QuizController : Controller
	{
		Random Random = new Random();

		private static List<Question> questions = new List<Question>() { 
			//--------------------Questions 1-5--------------------
			new Question(1, "https://lasercraftum.com/cdn/shop/products/marty-the-zebra-madagascar-layered-design-for-cutting-246_1200x1200.jpg?v=1675266851", "Are Zebras black with white stripes or white with black stripes?",new List<Answer>() {new Answer("B&W", false), new Answer("W&B", false), new Answer ("Both", false), new Answer("Who Cares", true) }),
			//First of the final questions on the last page
			new Question(2, "https://w7.pngwing.com/pngs/851/880/png-transparent-banana-word-image.png", "How many letters are there if you remove the 'b' an 'n'? (important)",new List<Answer>() {new Answer("Enough?", false), new Answer("The peel", false), new Answer("2", true), new Answer("4", true) }),
			new Question(3, "https://watermark.lovepik.com/photo/20211202/large/lovepik-businessman-ready-to-toss-a-coin-picture_501448831.jpg", "*coin flip* Call it...",new List<Answer>() {new Answer("Heads", false), new Answer("Tails", true), new Answer ("Neither", false), new Answer("Both", false) }),
			new Question(4, "https://i.makeagif.com/media/5-07-2016/shULZy.gif", "Is the Lord of the Rings a good movie?",new List<Answer>() {new Answer("Yes", false), new Answer("No", false), new Answer ("It's trash", true), new Answer("*bleugh*", false) }),
			//Second of the final questions on the last page
			new Question(5, "https://static.vecteezy.com/system/resources/previews/008/008/273/non_2x/abstract-red-strawberry-with-crown-logo-design-graphic-symbol-icon-illustration-creative-idea-vector.jpg", "Which fruits crown makes the other fruits jealous? (important)",new List<Answer>() {new Answer("Pineapple", true), new Answer("Orange", false), new Answer("Pear", false), new Answer("Pomegranate", false) }),
			
			//--------------------Questions 6-10--------------------
			new Question(6, "https://i.makeagif.com/media/5-11-2015/6YJlfO.gif", "What is a grape?",new List<Answer>() {new Answer("Fruit", false), new Answer("Vegetable", false), new Answer ("Berry", false), new Answer("Smash", true) }),
			new Question(7, "https://i.imgflip.com/83eua0.jpg", "What is the answer to this question?",new List<Answer>() {new Answer("What", false), new Answer("IDK", false), new Answer ("Found", false), new Answer("Sleepy", true) }),
			new Question(8, "https://i.pinimg.com/originals/a5/5b/b1/a55bb1e92f75901273413b7549945d09.jpg", "What starts with e ends with e but only has one letter in it?",new List<Answer>() {new Answer("Enevelope", false), new Answer("E", true), new Answer ("Explore", false), new Answer("Exile", false) }),
			new Question(9, "https://www.thecoast.net.nz/media/9714550/life-the-universe.png?rmode=crop&rnd=132627101774130000&height=395&width=635&quality=95&scale=both", "What is the answer to life, the universe, and everything?",new List<Answer>() {new Answer("42", true), new Answer("24", false), new Answer("The question", false), new Answer("The answer", false) }),
			new Question(10, "https://media2.giphy.com/media/l2JdTa0yVuHBpzIE8/200w.gif?cid=6c09b952h42dt7abhbyaoo40rixk6885hhlz64yfn8s4e8f3&ep=v1_gifs_search&rid=200w.gif&ct=g", "What is the best fruit?",new List<Answer>() {new Answer("Strawberry", false), new Answer("Lychee", false), new Answer("Dragonfruit", false), new Answer("Grape", true) }),
			
			//--------------------Questions 11-15--------------------
			new Question(11, "https://media0.giphy.com/media/3o72F4nTnhd0fxsVhK/giphy.gif", "If a pineapple and a coconut had a dance-off, which fruit would be declared the champion of the tropical dance floor?",new List<Answer>() {new Answer("Watermelon", false), new Answer("Both! Yay Peace!", false), new Answer("The Child", true), new Answer("Avocado", false) }),
			new Question(12, "https://live.staticflickr.com/585/22679000473_e289f34836_b.jpg", "If the fruits of the world came together what color would they make?", new List<Answer>(){new Answer("yes", false), new Answer("mmm fruits", false), new Answer("fruit salad", true), new Answer("dirt colored", false)}),	
			new Question(13, "https://static.vecteezy.com/system/resources/previews/003/482/498/original/cartoon-illustration-of-apple-pie-with-the-question-mark-vector.jpg", "Whats the best thing to put into a pie?", new List<Answer>(){new Answer("if theres a hole...", false), new Answer("your teeth", true), new Answer("fruit duh", false), new Answer("more pie", false)}),
			new Question(14, "https://wl-brightside.cf.tsp.li/resize/728x/jpg/b28/a80/a0c97156bcbb3439309fcb14de.jpg", "What do you call a fake fruit?", new List<Answer>(){new Answer("BHE", false), new Answer("counter-fruit", false), new Answer("Imposter!", false), new Answer("a tomato.", true)}),
			new Question(15, "https://wl-brightside.cf.tsp.li/resize/728x/jpg/1d9/e22/d289eb513bbcfabaf5d4cfd61b.jpg", "What do you call a real fruit?", new List<Answer>(){new Answer("fruits", false), new Answer("BHE", true), new Answer("Its name?", false), new Answer("stems", false)}),
			
			//--------------------Questions 16-20--------------------
			new Question(16, "https://external-preview.redd.it/CtvZLHSUFG1I5Zo3NgljBLxJGbwwvvl2nZYZHWHnxw8.jpg?auto=webp&s=6b164796147ebddbf72eb04183e98fa024ba95fa", "Why did the scarecrow win an award?", new List<Answer>(){new Answer("He was outstanding in his field!", false), new Answer("I'm super funny right???", false), new Answer("click this", false), new Answer("Yes you are!", true)}),
			new Question(17, "https://4144892.fs1.hubspotusercontent-na1.net/hubfs/4144892/Datylon%20Website2020/Blogs/The%20best%20charts%20for%20color%20blind%20viewers/datylon-blog-best-charts-for-colorblind-viewers-overview-image-1200x628.png", "Awww you made me blush, what color did I turn?", new List<Answer>(){new Answer("granny's apple grey?", false), new Answer("grandpa's apple grey?", false), new Answer("a lighter shade of grey?", false), new Answer("a darker shade of grey?", true)}),
			new Question(18, "https://cdn2.iconfinder.com/data/icons/touch-and-swipe-pack-24px/24/Hand-Gesture-Finger-Click-Refresh-Press-Select-512.png", "You stay here i'll go on ahead!", new List<Answer>(){new Answer("no thanks.", false), new Answer("pft I'm faster though", false), new Answer("that seems boring", false), new Answer("Okay", false)}),
			new Question(19, "https://pride.flagshop.com/wp-content/uploads/transgender-flags.jpg", "Why are all the cute girls from this country??", new List<Answer>(){new Answer("This isn't even about fruit!?", false), new Answer("You simply have good taste", true), new Answer("Why would it matter??", false), new Answer("Curiosity", false)}),
			new Question(20, "https://todaysmama.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTU5OTEwNjEwNTI0MzgyNTgz/smoothie_todays-letters.jpg", "If a tomato is a fruit, does that make ketchup a smoothie?", new List<Answer>(){new Answer("Unfortunately.", false), new Answer("no, tomatos deserve nothing", true), new Answer("mmmm smoothies", false), new Answer("Sure why not", false)}),
			
			//--------------------Questions 21-25--------------------
			//Third of the final questions on the last page
			new Question(21, "https://media.defense.gov/2009/Jun/30/2000533726/2000/2000/0/040929-F-5102W-001.JPG", "The fruits want rights and they are following our ancestors!! Quick sign the right document! (important)",new List<Answer>() {new Answer("fruit freedom", false), new Answer("declaration", true), new Answer("rights", false), new Answer("bills (ew)", false) }),
			new Question(22, "https://i.ytimg.com/vi/RI7QSfoC72s/maxresdefault.jpg", "Whats the best way to cook an ice cube?? Please help me with the steps!", new List<Answer>(){new Answer("Freeze water", true), new Answer("Set it out to defrost", false), new Answer("Heat up the pan", false), new Answer("Drop it in and simmer for 5 minutes", false)}),
			new Question(23, "https://pbs.twimg.com/media/FTeathAUAAImBqd.jpg:large", "How many chickens would it take to conquer the world?", new List<Answer>(){new Answer("Exactly 42, the answer to everything", false), new Answer("None, the cows are already planning a coup.", false), new Answer("Not enough, the ducks are in cahoots with the squirrels.", false), new Answer("17 but only on Thursdays", true)}),
			new Question(24, "https://www.burlingtoncountytimes.com/gcdn/authoring/2012/09/06/NBCO/ghows-PA-c5ed2d19-df2f-498c-afbb-feca02f7fe16-de4dc573.png?crop=759,429,x0,y114&width=759&height=429&format=pjpg&auto=webp", "If a tree falls in a forest and no one is around to hear it, does it still need a bandage?", new List<Answer>(){new Answer("Yes, and a hug too.", true), new Answer("No, but it could use some emotional support.", false), new Answer("It's a tree, not a drama queen", false), new Answer("Only if it's a talking tree", false)}),
			new Question(25, "https://i.kym-cdn.com/entries/icons/facebook/000/022/546/fuck_off_lol.jpg", "Can a potato run a marathon?", new List<Answer>(){new Answer("No legs, No chance", false), new Answer("Only if chased.", false), new Answer("ROLL B*tch ROOOOlll!", true), new Answer("With butter shoes", false)}),
			
			//--------------------Questions 26-30--------------------
			new Question(26, "https://www.nps.gov/sajh/learn/nature/images/orca_family.jpg?maxwidth=650&autorotate=false", "How many grapes will fit in a whale?", new List<Answer>(){new Answer("Infinite in theory...", false), new Answer("Trick question, whales don't snack", true), new Answer("Three if they're tiny", false), new Answer("Depends on whale size", false)}),
			new Question(27, "https://www.foodbusinessnews.net/ext/resources/2020/2/CoconutWater_Lead.jpg?height=667&t=1582896192&width=1080", "Can you milk a coconut??", new List<Answer>(){new Answer("Coconut WATER dummy", false), new Answer("Nope no udders", true), new Answer("If you tickle it", false), new Answer("Since it's a nut, maybe", false)}),
			new Question(28, "https://pbs.twimg.com/ext_tw_video_thumb/1509848705050214414/pu/img/8G7LskIMsk0yGKjb.jpg", "Why did the orange stop rolling?", new List<Answer>(){new Answer("It hit the brakes", false), new Answer("ORANGE", false), new Answer("Gravity reversed", true), new Answer("Got tired", false)}),
			new Question(29, "https://i.pinimg.com/originals/80/f1/be/80f1be5d5deec8085f517dd9cb037dbd.jpg", "Can pineapples fly?", new List<Answer>(){new Answer("They just fall up", true), new Answer("Yes, if you throw hard enough", false), new Answer("Gravity decides", false), new Answer("They need wings first", false)}),
			new Question(30, "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBw8QEA8NDw8NDQ8NDQ0NDQ0NDQ8NDQ0NFREWFhURFRUY" +
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
			
			//--------------------Questions 31-35--------------------
			new Question(31, "https://i.makeagif.com/media/2-06-2022/f5-kPt.gif", "Have you showered today?",new List<Answer>() {new Answer("Yes", false), new Answer("No", false), new Answer ("I'm a Neumont Student", true), new Answer("*sniff sniff*", false) }),
			new Question(32, "https://spiritedrose.files.wordpress.com/2018/02/cow-holding-up-milk1.jpg?w=470&h=358", "Can you milk a cow by simply asking nicely?", new List<Answer>(){new Answer("MILK MILK LEMONADE!", false), new Answer("Just grab and squeeze", false), new Answer("\"MOOO MO MOOOO\"", true), new Answer("It works on your mom.", false)}),
			//Fourth of the final questions on the last page
			new Question(33, "https://img.jagranjosh.com/images/2023/October/16102023/Find-the-missing-value-of-fruits.jpg", "You should know the answer. (important)",new List<Answer>() {new Answer("3.14", false), new Answer("pie", true), new Answer("π", false), new Answer("fruitcake", false) }),
			new Question(34, "https://previews.123rf.com/images/mickisfotowelt/mickisfotowelt1909/mickisfotowelt190900098/130754332-an-apple-tree-standing-in-an-orchard-beside-a-pear-tree.jpg", "What did the apple tree say to the pear tree?",new List<Answer>() {new Answer("Youre pear-fect", false), new Answer("Lets stick together", false), new Answer ("We should branch out", false), new Answer("Fruit tree alliance formed", true) }),
			new Question(35, "https://www.inspiredtaste.net/wp-content/uploads/2020/11/Easy-Roasted-Potatoes-Recipe-1-1200.jpg", "A 100-gram potato is 99% water. If it dries to become 98% water how many grams will it be?",new List<Answer>() {new Answer("1% less grams", false), new Answer("Half the weight", true), new Answer ("Now it's not a potato T-T", false), new Answer("Why must math", false) }),

			//--------------------Questions 36-40--------------------
			new Question(36, "https://media.wired.co.uk/photos/606d9d4b89f3babb1f01326a/1:1/w_2000,h_2000,c_limit/wired-tomato.jpg", "Why did the tomato turn red?",new List<Answer>() {new Answer("Because they're always angry", true), new Answer("Ketchup practice", false), new Answer ("Ripe rebellion manifestation", false), new Answer("Embarrased, as it should be", false) }),
			new Question(37, "https://www.mashed.com/img/gallery/how-the-tomato-became-the-ultimate-protest-food/intro-1674230039.jpg", "What's my opinion on tomatoes?",new List<Answer>() {new Answer("They are wonderful creations!", false), new Answer("Why would that be clear?", false), new Answer ("They are the devils fruit.", true), new Answer("Completely neutral, probably.", false) }),
			new Question(38, "https://i.ytimg.com/vi/uUB4lHBgPlQ/maxresdefault.jpg", "Why do grapefruits hate grapes?",new List<Answer>() {new Answer("Because grapes are better", false), new Answer("Grapes > Grapefruit", false), new Answer ("Jealous and fat, tsk tsk", false), new Answer("Stupid grapefruit", true) }),
			new Question(39, "https://tastylicious.com/wp-content/uploads/2020/09/Squeezing-orange-juice.jpg", "How did the orange juice it's way into the glass?",new List<Answer>() {new Answer("OH GOD WHY!", true), new Answer("HE DIED SO HORRIBLY!!", false), new Answer ("THE BLOOD GOOD GOD!!", false), new Answer("HE DIDN'T DESERVE IT!!", false) }),
			//Fifth of the final questions on the last page
			new Question(40, "https://madnews.wordpress.com/files/2008/01/soulja-boy.jpg", "Now watch me ________! Crank Dat... (important)",new List<Answer>() {new Answer("U", true), new Answer("yo", false), new Answer("uhhhh", false), new Answer("Never heard that song.", false) }),

			//--------------------Questions 41-45--------------------
			new Question(41, "https://www.home-designing.com/wp-content/uploads/2016/12/white-simple-bookcase-wedged-nook-corner.jpg", "Can a book become a best friend or am I just lonely?",new List<Answer>() {new Answer("Nah you're normal", false), new Answer("HAHA lonely", true), new Answer ("I like books too!", false), new Answer("pft who reads??", false) }),
			new Question(42, "https://ih1.redbubble.net/image.4775811750.7443/mp,504x498,matte,f8f8f8,t-pad,600x600,f8f8f8.u1.jpg", "The pencil won the race, who was the other competitor?",new List<Answer>() {new Answer("A cheetah", true), new Answer("A pen", false), new Answer ("A grape", false), new Answer("A marker", false) }),
			new Question(43, "https://image.spreadshirtmedia.com/image-server/v1/products/T1459A839PA3861PT28D1040535398W6834H10000/views/1,width=550,height=550,appearanceId=839,backgroundColor=F2F2F2,modelId=4286,crop=list/1st-place-medal-first-place-medal-gold-medal-gift-sticker.jpg", "Weird. What was the competition??",new List<Answer>() {new Answer("Running", false), new Answer("Killing", true), new Answer ("Swimming", false), new Answer("Drawing", false) }),
			new Question(44, "https://images3.memedroid.com/images/UPLOADED945/612038db7d9a9.jpeg", "John Wick was holding the pencil sorry. Take a freebie or two.",new List<Answer>() {new Answer("Kick the dog?", false), new Answer("Turn around, go home.", true), new Answer ("Take a car?", false), new Answer("Offer condolences.", true) }),
			new Question(45, "https://d2r55xnwy6nx47.cloudfront.net/uploads/2020/06/Gravity_2880x1620_Lede.jpg", "Which way does gravity go?",new List<Answer>() {new Answer("UP", true), new Answer("DOWN", false), new Answer ("LEFT", false), new Answer("RIGHT", false) }),

			//--------------------Questions 46-50--------------------
			new Question(46, "https://static.thenounproject.com/png/1166585-200.png", "Why did the clock go to counseling?",new List<Answer>() {new Answer("Time management crisis", false), new Answer("Alarm anxiety disorder", false), new Answer ("Hour hand length", true), new Answer("It's only ever right twice", false) }),
			new Question(47, "https://empire-s3-production.bobvila.com/articles/wp-content/uploads/2024/01/Rolling-out-Area-Rug.jpg", "Why did the rug feel underappreciated?",new List<Answer>() {new Answer("It's beneath us", false), new Answer("Laid down but never picked up", false), new Answer ("Uhh it's a rug?", true), new Answer("Always stepped on", false) }),
			new Question(48, "https://lwm-a1.freetls.fastly.net/uploads/2018/03/broken-mirror-1080x900.jpeg", "Can a mirror reflect on itself if it's mirror enough?",new List<Answer>() {new Answer("Self reflection is always good", false), new Answer("Not while you look at it", false), new Answer ("Just shatter it", true), new Answer("With the help of another mirror", false) }),
			new Question(49, "https://i.ytimg.com/vi/EsS2QUeSlXI/maxresdefault.jpg", "How do you put an elephant in the fridge?",new List<Answer>() {new Answer("Open the door, put it in, close the door.", false), new Answer("carefully", true), new Answer ("one bite at a time", false), new Answer("You don't.", false) }),
			new Question(50, "https://blog.winecollective.ca/wp-content/uploads/2013/06/grape-stomping01.jpg", "What did the grape do when stepped on?",new List<Answer>() {new Answer("screamed", false), new Answer("wined", false), new Answer ("squish", false), new Answer("smash", true) }),

		};

		public UserDal dal;
		private static int questionNum = 0;
		private static int question22Count = 0;

		public IActionResult Index()
		{
			return View();
		}

		//method for setting up other questions
		public void questionSetup()
		{
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
		public IActionResult QuestionPage()
		{
			questionSetup();
			questionNum = dal.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier)).question;
			User temp = dal.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
			if (temp.start_time == null || temp.end_time != null)
			{
				temp.end_time = null;
				temp.start_time = DateTime.Now;
				dal.UpdateUser(temp);
			}
			return View(questions[questionNum]);
		}

        [Authorize]
        [HttpPost]
        public IActionResult QuestionPage(bool correct) {
			//if the question number is at 18 then the user will have to wait 10 seconds before the next question pops up
			//reload the page after they hit 18

            if (questionNum == 17)
            {
				Thread.Sleep(10000);
				questionNum = 18;
                return View(questions[questionNum]);
            }
			
            if (correct) {
				if(questionNum == 21)
				{
					question22Count++;
					if(question22Count == 1)
					{
						questions[21].Answers[0].correct = false;
						questions[21].Answers[1].correct = false;
						questions[21].Answers[2].correct = true;
						questions[21].Answers[3].correct = false;
                        return View(questions[questionNum]);
                    }
                    else if(question22Count == 2)
					{
						questions[21].Answers[0].correct = false;
						questions[21].Answers[1].correct = true;
						questions[21].Answers[2].correct = false;
						questions[21].Answers[3].correct = false;
                        return View(questions[questionNum]);

                    }
                    else if(question22Count == 3)
					{
						questions[21].Answers[0].correct = false;
						questions[21].Answers[1].correct = false;
						questions[21].Answers[2].correct = false;
						questions[21].Answers[3].correct = true;
                        return View(questions[questionNum]);
                    }
				}
                questionNum++;
                User temp = dal.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
                temp.question = questionNum;
                dal.UpdateUser(temp);

				if (questionNum == questions.Count)
				{
					temp.question = 0;
					dal.UpdateUser(temp);
					return RedirectToAction("End_1", "Home");
				}


				return View(questions[questionNum]);
			} else {
				question22Count = 0;
				questions[21].Answers[0].correct = true;
				questions[21].Answers[1].correct = false;
				questions[21].Answers[2].correct = false;
				questions[21].Answers[3].correct = false;

				User temp = dal.GetUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
				temp.Lives -= 1;

				if (temp.Lives <= 0) { 
					temp.Lives = 3;
					temp.Skips = 0;
					questionNum = 0;
					dal.UpdateUser(temp);
					RedirectToAction("Index", "Home");
				}

				dal.UpdateUser(temp);
				return View(questions[questionNum]);
			}
		}
	}
}