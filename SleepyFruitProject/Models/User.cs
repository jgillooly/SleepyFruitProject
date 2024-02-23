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

		protected static DateTime start_time;
		protected static DateTime end_time;
		public TimeSpan ElapsedTime = end_time - start_time;

		User(int userId, string userName, string email, TimeSpan elapsedTime)
		{
			this.ID = userId;
			this.UserName = userName;
			this.Email = email;
			this.ElapsedTime = elapsedTime;
		}
		User() 
		{
		}
	}

}
