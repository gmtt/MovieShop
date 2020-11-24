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
			var casts = await _castRepository.GetCastsWithMovie(id);
			throw new System.NotImplementedException();
		}
	}
}