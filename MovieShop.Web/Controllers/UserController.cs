using Microsoft.AspNetCore.Mvc;
using MovieShop.Web.Models;

namespace MovieShop.Web.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Create()
		{
			return View();
		}

		public IActionResult Details(int userId)
		{
			return View();
		}
	}
}