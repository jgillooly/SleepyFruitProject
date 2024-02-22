using System.ComponentModel.DataAnnotations;

namespace SleepyFruitProject.Models
{
	public class Answer
	{ 
		public string TheAnswer { get; set; }

		public bool correct;

		public Answer() { }

		public Answer(string answer, bool correct) 
		{
			this.TheAnswer = answer;
			this.correct = correct;
		}
	}
}
