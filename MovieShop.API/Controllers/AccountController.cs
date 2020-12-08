using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MovieShop.Core.Models.Request;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IUserService _userService;

		public AccountController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetUserDetails(int id)
		{
			var user = await _userService.GetUserDetails(id);
			if (user == null) return BadRequest(new {message = "User not exist!"});
			return Ok(user);
		}

		[HttpPost]
		[Route("")]
		public async Task<IActionResult> RegisterUser(UserRegisterRequestModel model)
		{
			if (ModelState.IsValid)
			{
				await _userService.CreateUser(model);
				return Ok(model);
			}

			return BadRequest(new {message = "Please correct the input information"});
		}

		[HttpPost]
		[Route("login")]
		public async Task<IActionResult> Login(LoginRequestModel model)
		{
			if (!ModelState.IsValid) return BadRequest(new {message = "Please correct the input information"});
			var user = await _userService.ValidateUser(model.Email, model.Password);
			if (user == null)
			{
				return BadRequest(new {message = "Invalid login attempt"});
			}
			return Ok(user);
		}
	}
}