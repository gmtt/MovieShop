using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration.Conventions;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		private readonly IMovieService _movieService;

		public MoviesController(IMovieService movieService)
		{
			_movieService = movieService;
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetMovieByIdRequest(int id)
		{
			var movie = await _movieService.GetMovieAsync(id);
			if (movie == null) return NotFound("Movie Not Found");
			return Ok(movie);
		}

		[HttpGet]
		[Route("toprated")]
		public async Task<IActionResult> GetTopRatedMovieRequest()
		{
			var movies = await _movieService.GetTopRatedMovies();
			return Ok(movies);
		}

		[HttpGet]
		[Route("toprevenue")]
		public async Task<IActionResult> GetTopRevenueMovies()
		{
			var movies = await _movieService.GetHighestGrossingMovies();

			if (!movies.Any())
			{
				return NotFound("No Movies Found");
			}

			return Ok(movies);
		}

		[HttpGet]
		[Route("genre/{genreId:int}")]
		public async Task<IActionResult> GetMovieByGenreId(int genreId)
		{
			var genre = await _movieService.GetMoviesByGenre(genreId);
			return Ok(genre);
		}

		[HttpGet]
		[Route("{id:int}/reviews")]
		public async Task<IActionResult> GetReviewsByMovieId(int id)
		{
			var reviews = await _movieService.GetReviewsForMovie(id);
			return Ok(reviews);
		}
	}
}