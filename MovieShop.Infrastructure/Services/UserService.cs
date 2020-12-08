using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MovieShop.Core.Entities;
using MovieShop.Core.Helpers;
using MovieShop.Core.Models;
using MovieShop.Core.Models.Request;
using MovieShop.Core.Models.Response;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Core.ServiceInterfaces;

namespace MovieShop.Infrastructure.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly ICryptoService _cryptoService;
		private readonly IPurchaseRepository _purchaseRepository;
		private readonly IMovieService _movieService;
		private readonly IAsyncRepository<Favorite> _favoriteRepository;
		private readonly IAsyncRepository<Review> _reviewRepository;

		public UserService(IUserRepository userRepository,
			ICryptoService cryptoService,
			IPurchaseRepository purchaseRepository,
			IMovieService movieService,
			IAsyncRepository<Favorite> favoriteRepository,
			IAsyncRepository<Review> reviewRepository
		)
		{
			_cryptoService = cryptoService;
			_userRepository = userRepository;
			_purchaseRepository = purchaseRepository;
			_movieService = movieService;
			_favoriteRepository = favoriteRepository;
			_reviewRepository = reviewRepository;
		}

		public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
		{
			var user = await _userRepository.GetUserByEmail(email);
			if (user == null) return null;
			var hashedPassword = _cryptoService.HashPassword(password, user.Salt);
			var isSuccess = user.HashedPassword == hashedPassword;
			var response = new UserLoginResponseModel
			{
				Id = user.Id,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				DateOfBirth = user.DateOfBirth,
			};
			return isSuccess ? response : null;
		}

		public async Task<UserRegisterResponseModel> CreateUser(UserRegisterRequestModel requestModel)
		{
			var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
			if (dbUser != null &&
			    string.Equals(dbUser.Email, requestModel.Email, StringComparison.CurrentCultureIgnoreCase))
				throw new Exception("Email Already Exits");
			var salt = _cryptoService.CreateSalt();
			var hashedPassword = _cryptoService.HashPassword(requestModel.Password, salt);
			var user = new User
			{
				Email = requestModel.Email,
				Salt = salt,
				HashedPassword = hashedPassword,
				FirstName = requestModel.FirstName,
				LastName = requestModel.LastName
			};
			var createdUser = await _userRepository.AddAsync(user);
			var response = new UserRegisterResponseModel
			{
				Id = createdUser.Id, Email = createdUser.Email, FirstName = createdUser.FirstName,
				LastName = createdUser.LastName
			};
			return response;
		}

		public async Task<UserRegisterResponseModel> GetUserDetails(int id)
		{
			var dbUser = await _userRepository.GetByIdAsync(id);
			if (dbUser == null) return null;
			var resp = new UserRegisterResponseModel()
			{
				Id = dbUser.Id,
				Email = dbUser.Email,
				FirstName = dbUser.FirstName,
				LastName = dbUser.LastName,
			};
			return resp;
		}

		public async Task<User> GetUser(string email)
		{
			throw new System.NotImplementedException();
		}

		public async Task<PagedResultSet<User>> GetAllUsersByPagination(int pageSize = 20, int page = 0,
			string lastName = "")
		{
			throw new System.NotImplementedException();
		}

		public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
		{
			if (await FavoriteExists(favoriteRequest.UserId, favoriteRequest.MovieId))
			{
				throw new HttpRequestException("Favorite Exist");
			}

			var favorite = new Favorite()
			{
				MovieId = favoriteRequest.MovieId,
				UserId = favoriteRequest.UserId
			};
			await _favoriteRepository.AddAsync(favorite);
		}

		public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
		{
			var favorite = await _favoriteRepository.ListAsync(f =>
				f.UserId == favoriteRequest.UserId && f.MovieId == favoriteRequest.MovieId);
			await _favoriteRepository.DeleteAsync(favorite.First());
		}

		public async Task<bool> FavoriteExists(int id, int movieId)
		{
			return await _favoriteRepository.GetExistsAsync(f => f.UserId == id && f.MovieId == movieId);
		}

		public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
		{
			var favorites = await _favoriteRepository.ListAllWithIncludesAsync(f => f.UserId == id, f => f.Movie);
			var favoriteMovies = favorites.Select(f => new FavoriteResponseModel.FavoriteMovieResponseModel()
			{
				Id = f.MovieId,
				PosterUrl = f.Movie.PosterUrl,
				ReleaseDate = f.Movie.ReleaseDate,
				Title = f.Movie.Title
			}).ToList();
			return new FavoriteResponseModel()
			{
				UserId = id,
				FavoriteMovies = favoriteMovies
			};
		}

		public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest)
		{
			if (await IsMoviePurchased(purchaseRequest))
			{
				throw new HttpRequestException("Movie Already Purchased");
			}

			var movie = await _movieService.GetMovieAsync(purchaseRequest.MovieId);
			purchaseRequest.TotalPrice = movie.Price;
			var purchase = new Purchase()
			{
				MovieId = purchaseRequest.MovieId,
				UserId = purchaseRequest.UserId,
				PurchaseDateTime = purchaseRequest.PurchaseDateTime,
				TotalPrice = (decimal) purchaseRequest.TotalPrice,
				PurchaseNumber = purchaseRequest.PurchaseNumber
			};
			await _purchaseRepository.AddAsync(purchase);
		}

		public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest)
		{
			return await _purchaseRepository.GetExistsAsync(p =>
				p.UserId == purchaseRequest.UserId && p.MovieId == purchaseRequest.MovieId);
		}

		public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int id)
		{
			var purchases = await _purchaseRepository.ListAllWithIncludesAsync(p => p.UserId == id, p => p.Movie);
			var purchaseMovies = purchases.Select(p => new PurchaseResponseModel.PurchasedMovieResponseModel()
			{
				PurchaseDateTime = p.PurchaseDateTime,
				Id = p.MovieId,
				Title = p.Movie.Title,
				PosterUrl = p.Movie.PosterUrl,
				ReleaseDate = p.Movie.ReleaseDate
			}).ToList();
			return new PurchaseResponseModel()
			{
				UserId = id,
				PurchasedMovies = purchaseMovies
			};
		}

		public async Task AddMovieReview(ReviewRequestModel reviewRequest)
		{
			var review = new Review()
			{
				MovieId = reviewRequest.MovieId,
				Rating = (decimal) reviewRequest.Rating,
				ReviewText = reviewRequest.ReviewText,
				UserId = reviewRequest.UserId
			};
			await _reviewRepository.AddAsync(review);
		}

		public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
		{
			var review = new Review()
			{
				MovieId = reviewRequest.MovieId,
				Rating = (decimal) reviewRequest.Rating,
				ReviewText = reviewRequest.ReviewText,
				UserId = reviewRequest.UserId
			};
			await _reviewRepository.UpdateAsync(review);
		}

		public async Task DeleteMovieReview(int userId, int movieId)
		{
			var review = await _reviewRepository.ListAsync(r => r.UserId == userId && r.MovieId == movieId);
			var enumerable = review.ToList();
			if (enumerable.Any())
				await _reviewRepository.DeleteAsync(enumerable.First());
		}

		public async Task<ReviewResponseModel> GetAllReviewsByUser(int id)
		{
			var reviews = await _reviewRepository.ListAllWithIncludesAsync(r => r.UserId == id, r => r.User);
			var reviewMovies = reviews.Select(r => new ReviewMovieResponseModel()
			{
				UserId = r.UserId,
				MovieId = r.MovieId,
				ReviewText = r.ReviewText,
				Rating = r.Rating,
				Name = r.User.FirstName + " " + r.User.LastName
			}).ToList();
			return new ReviewResponseModel()
			{
				MovieReviews = reviewMovies,
				UserId = id
			};
		}
	}
}