using System.Linq;
using System.Threading.Tasks;
using MovieShop.Core.Models;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
	public class CastService : ICastService
	{
		private readonly ICastRepository _castRepository;

		public CastService(ICastRepository castRepository)
		{
			_castRepository = castRepository;
		}
		public async Task<CastDetailsResponseModel> GetCastDetailsWithMovies(int id)
		{
			var cast = await _castRepository.GetByIdAsync(id);
			if (cast == null) return null;
			var resp = new CastDetailsResponseModel()
			{
				Id = cast.Id,
				Gender = cast.Gender,
				Name = cast.Name,
				ProfilePath = cast.ProfilePath,
				TmdbUrl = cast.TmdbUrl
			};
			return resp;
		}
	}
}