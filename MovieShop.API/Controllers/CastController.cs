using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.ServiceInterfaces;
using MovieShop.Infrastructure.Services;

namespace MovieShop.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CastController : ControllerBase
	{
		private ICastService _castService;

		public CastController(ICastService castService)
		{
			_castService = castService;
		}
		[HttpGet]
		[Route("{id:int}")]
		public async Task<IActionResult> GetCastRequest(int id)
		{
			var resp = await _castService.GetCastDetailsWithMovies(id);
			if (resp == null) NotFound("Not Found");
			return Ok(resp);
		} 
	}
}
