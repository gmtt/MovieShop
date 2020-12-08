using System.ComponentModel.DataAnnotations;

namespace MovieShop.Core.Models.Request
{
	public class CastRequestModel
	{
		[Required] [StringLength(50)] public string Name { get; set; }
		[Required] public string Gender { get; set; }
		public string TmdbUrl { get; set; }
		public string ProfilePath { get; set; }
	}
}