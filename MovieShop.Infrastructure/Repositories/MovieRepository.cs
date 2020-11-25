using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repositories
{
	public class MovieRepository : EFRepository<Movie>, IMovieRepository
	{
		public MovieRepository(MovieShopDbContext dbContext) : base(dbContext) { }

		public override async Task<Movie> GetByIdAsync(int id)
		{
			var movie = await _dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
			if (movie == null) return null;
			var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
				.AverageAsync(r => r == null ? 0 : r.Rating);
			if (movieRating > 0) movie.Rating = movieRating;
			return movie;
		}

		public async Task<IEnumerable<Movie>> GetTopRatedMovies()
		{
			return await _dbContext.Reviews
				.GroupBy(r => r.MovieId)
				.OrderByDescending(g => g.Average(r => r.Rating))
				.SelectMany(r => r)
				.Select(r => r.Movie)
				.Take(50)
				.ToListAsync();
		}

		public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
		{
			return await _dbContext.MovieGenres
				.Where(mg => mg.GenreId == genreId)
				.Select(mg => mg.Movie)
				.ToListAsync();
		}

		public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
		{
			return await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(50).ToListAsync();
		}
	}
}