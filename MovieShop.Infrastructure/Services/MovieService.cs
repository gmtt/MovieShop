﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MovieShop.Core.Entities;
using MovieShop.Core.Helpers;
using MovieShop.Core.Models;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
	public class MovieService : IMovieService
	{
		private readonly IMovieRepository _movieRepository;
		private readonly IPurchaseRepository _purchaseRepository;


		public MovieService(IMovieRepository movieRepository)
		{
			_movieRepository = movieRepository;
		}

		public MovieService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
		{
			this._movieRepository = movieRepository;
			this._purchaseRepository = purchaseRepository;
		}

		public async Task<PagedResultSet<MovieResponseModel>> GetMoviesByPagination(int pageSize = 20, int page = 0,
			string title = "")
		{
			throw new System.NotImplementedException();
		}

		public async Task<PagedResultSet<MovieResponseModel>> GetAllMoviePurchasesByPagination(int pageSize = 20,
			int page = 0)
		{
			var totalPurchases = await _purchaseRepository.GetCountAsync();
			var purchases = await _purchaseRepository.GetAllPurchases(pageSize, page);
			var data = purchases.Select(p =>
			{
				var m = p.Movie;
				return new MovieResponseModel()
				{
					Id = m.Id,
					PosterUrl = m.PosterUrl,
					ReleaseDate = m.ReleaseDate,
					Title = m.Title
				};
			}).ToList();
			return new PagedResultSet<MovieResponseModel>(data, page, pageSize, totalPurchases);
		}

		public async Task<PaginatedList<MovieResponseModel>> GetAllPurchasesByMovieId(int movieId)
		{
			var purchases = await _purchaseRepository.GetAllPurchasesByMovie(movieId);
			throw new System.NotImplementedException();
		}

		public async Task<MovieDetailsResponseModel> GetMovieAsync(int id)
		{
			var movie = await _movieRepository.GetByIdAsync(id);
			var config = new MapperConfiguration(cfg => cfg.CreateMap<Movie, MovieDetailsResponseModel>());
			var mapper = config.CreateMapper();
			return mapper.Map<MovieDetailsResponseModel>(movie);
		}

		public async Task<IEnumerable<ReviewMovieResponseModel>> GetReviewsForMovie(int id)
		{
			var reviews = await _movieRepository.GetMovieReviews(id);
			var resp = reviews.Select(r => new ReviewMovieResponseModel()
			{
				MovieId = r.MovieId,
				Name = r.User.FirstName + " " + r.User.LastName,
				Rating = r.Rating,
				ReviewText = r.ReviewText,
				UserId = r.UserId
			});
			return resp;
		}

		public async Task<int> GetMoviesCount(string title = "")
		{
			var cnt = await _movieRepository.GetCountAsync();
			return cnt;
		}

		public async Task<IEnumerable<MovieResponseModel>> GetTopRatedMovies()
		{
			var movies = await _movieRepository.GetTopRatedMovies();
			var response = movies.Select(movie => new MovieResponseModel()
				{Id = movie.Id, PosterUrl = movie.PosterUrl, ReleaseDate = movie.ReleaseDate, Title = movie.Title});
			return response;
		}

		public async Task<IEnumerable<MovieResponseModel>> GetHighestGrossingMovies()
		{
			var movies = await _movieRepository.GetHighestRevenueMovies();
			var response = movies.Select(movie => new MovieResponseModel()
				{Id = movie.Id, PosterUrl = movie.PosterUrl, ReleaseDate = movie.ReleaseDate, Title = movie.Title});
			return response;
		}

		public async Task<IEnumerable<MovieResponseModel>> GetMoviesByGenre(int genreId)
		{
			var movies = await _movieRepository.GetMoviesByGenre(genreId);
			var response = movies.Select(movie => new MovieResponseModel()
				{Id = movie.Id, PosterUrl = movie.PosterUrl, ReleaseDate = movie.ReleaseDate, Title = movie.Title});
			return response;
		}

		public async Task<MovieDetailsResponseModel> CreateMovie(MovieCreateRequest movieCreateRequest)
		{
			var configMovie = new MapperConfiguration(cfg => cfg.CreateMap<MovieCreateRequest, Movie>());
			var mapperMovie = configMovie.CreateMapper();
			var movie = mapperMovie.Map<Movie>(movieCreateRequest);
			var entity = await _movieRepository.AddAsync(movie);
			var config = new MapperConfiguration(cfg => cfg.CreateMap<Movie, MovieDetailsResponseModel>());
			var mapper = config.CreateMapper();
			return mapper.Map<MovieDetailsResponseModel>(entity);
		}

		public async Task<MovieDetailsResponseModel> UpdateMovie(MovieCreateRequest movieCreateRequest)
		{
			var configMovie = new MapperConfiguration(cfg => cfg.CreateMap<MovieCreateRequest, Movie>());
			var mapperMovie = configMovie.CreateMapper();
			var movie = mapperMovie.Map<Movie>(movieCreateRequest);
			var entity = await _movieRepository.UpdateAsync(movie);
			var config = new MapperConfiguration(cfg => cfg.CreateMap<Movie, MovieDetailsResponseModel>());
			var mapper = config.CreateMapper();
			return mapper.Map<MovieDetailsResponseModel>(entity);
		}
	}
}