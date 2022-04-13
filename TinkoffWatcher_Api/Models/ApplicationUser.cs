using Microsoft.AspNetCore.Identity;
using System;

namespace TinkoffWatcher_Api.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public Sex Sex { get; set; }
	}

	public enum Sex
    { 
		Men,
		Women
	}
}
