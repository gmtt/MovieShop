using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.Models;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private IMovieService _movieService;

		public AdminController(IMovieService movieService)
		{
			_movieService = movieService;
		}

		[HttpPost]
		[Route("movie")]
		public async Task<IActionResult> AddNewMovie(MovieCreateRequest req)
		{
			if (!ModelState.IsValid) return BadRequest(new {message = "invalid input format"});
			var resp = await _movieService.CreateMovie(req);
			return Ok(resp);
		}

		[HttpPut]
		[Route("movie")]
		public async Task<IActionResult> UpdateMovie(MovieCreateRequest req)
		{
			if (!ModelState.IsValid) return BadRequest(new {message = "invalid input format"});
			var resp = await _movieService.UpdateMovie(req);
			return Ok(resp);
		}

		[HttpGet]
		[Route("purchases")]
		public async Task<IActionResult> GetAllPurchases([FromQuery] int pageSize = 30, [FromQuery] int page = 0)
		{
			var resp = await _movieService.GetAllMoviePurchasesByPagination(pageSize, page);
			return Ok(resp);
		}

		[HttpGet]
		[Route("top")]
		public async Task<IActionResult> GetTopMovies()
		{
			var resp = await _movieService.GetTopRatedMovies();
			return Ok(resp);
		}
	}
}