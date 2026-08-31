using Microsoft.EntityFrameworkCore;
using Source.Repositories.IRepositories;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;

namespace Source.Repositories
{
	/// <summary>
	///		Generic implementation of <see cref="IRepository{TKey, TEntity}"/>. Serves as the <see langword="base"/>
	///		<see langword="class"/> which all repositories inherits from.
	/// </summary>
	/// <remarks>
	///		All sub classes should call this <see cref="Context"/> rather than declaring their own DbContext field.
	/// </remarks>
	/// <typeparam name="TKey">The data type of the id property (i.e. the primary key in SQL databases) of <typeparamref name="TEntity"/>.</typeparam>
	/// <typeparam name="TEntity">The Model <see langword="class"/> this Repository works with.</typeparam>
	public class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
	{
		/// <summary>
		///		The DbContext which all sub classes should use to call the database.
		/// </summary>
		protected DbContext Context { get; private set; }

		/// <summary><inheritdoc cref="Repository{TKey, TEntity}"/></summary>
		/// <remarks><inheritdoc cref="Repository{TKey, TEntity}"/></remarks>
		/// <param name="context">A database context which inherits <see cref="DbContext"/>.</param>
		public Repository(DbContext context)
		{
			this.Context = context;
		}


		// Retrieve for READ or UPDATE
		public async Task<TEntity?> GetAsync(TKey id)
		{
			return await this.Context.Set<TEntity>().FindAsync(id);
		}

		public async Task<IList<TEntity>> GetAllAsync()
		{
			return await this.Context.Set<TEntity>().AsNoTracking().ToListAsync();
		}

		public async Task<IList<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>>? predicate)
		{
			var result = (predicate == null) ? await this.GetAllAsync() :
				await this.Context.Set<TEntity>()
					.Where(predicate)
					.AsNoTracking()
					.ToListAsync();
			return result;
		}

		public async Task<int> CountByFilterAsync(Expression<Func<TEntity, bool>>? predicate = null)
		{
			var result = (predicate == null) ?
				await this.Context.Set<TEntity>().CountAsync() :
				await this.Context.Set<TEntity>().Where(predicate).CountAsync();
			return result;
		}

		public async Task<T> CountByFilterAsync<T>(Expression<Func<TEntity, bool>>? predicate = null) where T : IBinaryInteger<T>
		{
			var result = (predicate == null) ?
				await this.Context.Set<TEntity>().CountAsync() :
				await this.Context.Set<TEntity>().Where(predicate).CountAsync();
			return (T)Convert.ChangeType(result, typeof(T), CultureInfo.InvariantCulture);
		}

		// CREATE
		public void Add(TEntity entity)
		{
			this.Context.Set<TEntity>().Add(entity);
		}

		public void AddRange(IEnumerable<TEntity> entites)
		{
			this.Context.Set<TEntity>().AddRange(entites);
		}

		// DELETE
		public void Remove(TEntity entity)
		{
			this.Context.Set<TEntity>().Remove(entity);
		}

		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			this.Context.Set<TEntity>().RemoveRange(entities);
		}

	}
}
