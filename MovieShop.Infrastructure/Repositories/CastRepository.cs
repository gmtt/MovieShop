using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repositories
{
	public class CastRepository : EFRepository<Cast>, ICastRepository
	{
		public CastRepository(MovieShopDbContext dbContext) : base(dbContext) { }
		public async Task<IEnumerable<Cast>> GetCastsWithMovie(int movieId)
		{
			return await _dbContext.MovieCasts
				.Where(mc => mc.MovieId == movieId)
				.Select(mc => mc.Cast)
				.ToListAsync();
		}
	}
}