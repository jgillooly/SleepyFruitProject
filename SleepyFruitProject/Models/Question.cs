namespace SleepyFruitProject.Models
{
	public class Question
	{
		public int num { get; set; }

		public string TheQuestion { get; set; }

		public List<Answer> Answers { get; set; }

		public string img { get; set; }
		public int lives { get; set; }
		public int skips { get; set; }

		public Question() { }

		public Question(int num, string img, string TheQuestion, List<Answer> Answers) 
		{
			this.num = num;
			this.img = img;
			this.TheQuestion = TheQuestion;
			this.Answers = Answers;
		}

	}
}
