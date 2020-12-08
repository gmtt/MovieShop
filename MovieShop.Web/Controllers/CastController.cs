using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Core.Models.Request;

namespace MovieShop.Web.Controllers
{
	public class CastController : Controller
	{
		public IActionResult Details(int castId)
		{
			return View();
		}

		[HttpGet]
		[Authorize]
		public IActionResult AddCast()
		{
			return View();
		}

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddCast(CastRequestModel model)
		{
			throw new NotImplementedException();
		}

	}
}