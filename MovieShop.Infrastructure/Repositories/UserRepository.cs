using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieShop.Core.Entities;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repositories
{
	public class UserRepository : EFRepository<User>, IUserRepository
	{
		public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
		{
		}

		public Task<User> GetUserByEmail(string email)
		{
			return _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
		}
	}
}