namespace SleepyFruitProject.Models
{
	public class Question
	{
		public string TheQuestion { get; set; }

		public List<Answer> Answers { get; set; }

		public string img { get; set; }

		public Question() { }

	}
}
