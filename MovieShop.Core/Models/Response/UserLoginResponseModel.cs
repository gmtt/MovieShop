using System;
using System.Collections.Generic;
using MovieShop.Core.Entities;

namespace MovieShop.Core.Models.Response
{
	public class UserLoginResponseModel
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public ICollection<Role> Roles { get; set; }
	}
}