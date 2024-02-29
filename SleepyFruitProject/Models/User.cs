using Microsoft.AspNetCore.Components.Forms;
using SleepyFruitProject.Areas.Identity.Pages.Account;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace SleepyFruitProject.Models
{
	public class User
	{
		[Key]
		public int ID { get; set; }

		public string? UserID { get; set; }
		[Required]
		public string UserName { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = "";

		public int question { get; set; } = 0;

		protected static DateTime start_time { get; set; }
		protected static DateTime end_time { get; set; }
		public TimeSpan ElapsedTime { get; set; } = end_time - start_time;

		public int Lives = 3;
		public int Skips = 0;

		public User(int userId, string userName, string email, TimeSpan elapsedTime)
		{
			this.ID = userId;
			this.UserName = userName;
			this.Email = email;
			this.ElapsedTime = elapsedTime;
		}

		public User(string UserID, string UserName, string Email)
		{
			this.UserID = UserID;
			this.UserName = UserName;
			this.Email = Email;
		}

		public User() 
		{
		}
	}

}
