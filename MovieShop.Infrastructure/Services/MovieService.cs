using System.Collections.Generic;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Models;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
	public class MovieService : IMovieService
	{
		private readonly IMovieRepository _movieRepository;
		private readonly IPurchaseRepository _purchaseRepository;
		public MovieService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
		{
			this._movieRepository = movieRepository;
			this._purchaseRepository = purchaseRepository;
		}

		public async Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(int pageSize = 20, int page = 0, string title = "")
		{
			throw new System.NotImplementedException();
		}

		public async Task<PagedResultSet<MovieResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20, int page = 0)
		{
			throw new System.NotImplementedException();
		}

		public async Task<PaginatedList<MovieResponseModel>> GetAllPurchasesByMovieId(int movieId)
		{
			var purchases = await _purchaseRepository.GetAllPurchasesByMovie(movieId);
			throw new System.NotImplementedException();
		}

		public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
		{
			var movies = await _movieRepository.GetByIdAsync(id);
			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
		{
			throw new System.NotImplementedException();
		}

		public async Task<int> GetMoviesCount(string title = "")
		{
			var cnt = await _movieRepository.GetCountAsync();
			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies()
		{
			var movies = await _movieRepository.GetTopRatedMovies();
			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies()
		{
			var movies = await _movieRepository.GetHighestRevenueMovies();
			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
		{
			var movies = await _movieRepository.GetMoviesByGenre(genreId);
			throw new System.NotImplementedException();
		}

		public async Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest)
		{
			Movie movie = new Movie();
			var entity = await _movieRepository.AddAsync(movie);
			throw new System.NotImplementedException();
		}

		public async Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest)
		{
			Movie movie = new Movie();
			var entity = await _movieRepository.UpdateAsync(movie);
			throw new System.NotImplementedException();
		}
	}
}