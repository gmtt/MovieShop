using Microsoft.EntityFrameworkCore;
using MovieShop.Core.RepositoryInterfaces;
using MovieShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieShop.Infrastructure.Repositories
{
	public class EFRepository<T> : IAsyncRepository<T> where T : class
	{
		protected readonly MovieShopDbContext _dbContext;

		public EFRepository(MovieShopDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public virtual async Task<T> GetByIdAsync(int id)
		{
			var entity = await _dbContext.Set<T>().FindAsync(id);
			return entity;
		}

		public async Task<IEnumerable<T>> ListAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public async Task<IEnumerable<T>> ListAllWithIncludesAsync(Expression<Func<T, bool>> @where,
			params Expression<Func<T, object>>[] includes)
		{
			var query = _dbContext.Set<T>().AsQueryable();

			if (includes == null) return await query.Where(@where).ToListAsync();
			query = includes.Aggregate(query, (current, navigationProperty) => current.Include(navigationProperty));

			return await query.Where(@where).ToListAsync();
		}

		public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter)
		{
			return await _dbContext.Set<T>().Where(filter).ToListAsync();
		}

		public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
		{
			if (filter != null)
			{
				return await _dbContext.Set<T>().Where(filter).CountAsync();
			}

			return await _dbContext.Set<T>().CountAsync();
		}

		public async Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null)
		{
			return filter != null && await _dbContext.Set<T>().Where(filter).AnyAsync();
		}

		public async Task<T> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}

		public async Task<T> UpdateAsync(T entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			// await _dbContext.Update(entity);
			await _dbContext.SaveChangesAsync();
			return entity;
		}

		public async Task DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}
	}
}