using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repositories
{
	public class GenreRepository : EFRepository<Genre>, IGenreRepository
	{
		public GenreRepository(MovieShopDbContext dbContext) : base(dbContext) { }
	}
}