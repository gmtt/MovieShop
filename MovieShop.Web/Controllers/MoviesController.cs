using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.Web.Controllers
{
	public class MoviesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult MovieByGenre(int genreId)
		{
			return View();
		}

		public IActionResult Details(int movieId)
		{
			return View();
		}
	}
}
