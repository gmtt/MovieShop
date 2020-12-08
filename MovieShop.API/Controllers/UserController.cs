using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		[Route("purchase")]
		public async Task<IActionResult> Purchase(PurchaseRequestModel model)
		{
			await _userService.PurchaseMovie(model);
			return Ok();
		}

		[HttpPost]
		[Route("favorite")]
		public async Task<IActionResult> Favorite(FavoriteRequestModel req)
		{
			await _userService.AddFavorite(req);
			return Ok();
		}

		[HttpPost]
		[Route("unfavorite")]
		public async Task<IActionResult> Unfavorite(FavoriteRequestModel req)
		{
			await _userService.RemoveFavorite(req);
			return Ok();
		}

		[HttpGet]
		[Route("{id:int}/movie/{movieId:int}/favorite")]
		public async Task<IActionResult> IsFavoriteExist(int id, int movieId)
		{
			var isExist = await _userService.FavoriteExists(id, movieId);
			return Ok(isExist);
		}

		[HttpPost]
		[Route("review")]
		public async Task<IActionResult> AddReview(ReviewRequestModel req)
		{
			await _userService.AddMovieReview(req);
			return Ok();
		}

		[HttpPut]
		[Route("review")]
		public async Task<IActionResult> UpdateReview(ReviewRequestModel req)
		{
			await _userService.UpdateMovieReview(req);
			return Ok();
		}

		[HttpDelete]
		[Route("{userId:int}/movie/{movieId:int}")]
		public async Task<IActionResult> DeleteReivew(int userId, int movieId)
		{
			await _userService.DeleteMovieReview(userId, movieId);
			return Ok();
		}

		[HttpGet]
		[Route("{id:int}/reviews")]
		public async Task<IActionResult> GetAllReviewsByUser(int id)
		{
			var reviews = await _userService.GetAllReviewsByUser(id);
			return Ok(reviews);
		}

		[HttpGet]
		[Route("{id:int}/purchases")]
		public async Task<IActionResult> GetAllPurchasesByUser(int id)
		{
			var purchases = await _userService.GetAllPurchasesForUser(id);
			return Ok(purchases);
		}

		[HttpGet]
		[Route("{id:int}/favorites")]
		public async Task<IActionResult> GetAllFavoritesByUser(int id)
		{
			var favorites = await _userService.GetAllFavoritesForUser(id);
			return Ok(favorites);
		}
	}
}