using Microsoft.AspNetCore.Mvc;

namespace MovieShop.Web.Controllers
{
	public class CastController : Controller
	{
		public IActionResult Details(int castId)
		{
			return View();
		}

	}
}