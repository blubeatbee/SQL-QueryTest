using System.Linq.Expressions;
using System.Numerics;

namespace Source.Repositories.IRepositories
{
	/// <summary>
	///		Interface for a generic Repository pattern. Contains basic data access methods that are shared by
	///		all components which inherits or implements this Interface. These methods should ONLY be defined
	///		by a single Repository <see langword="base"/> <see langword="class"/> which all other Repository
	///		patterns inherits.
	/// </summary>
	/// <remarks>
	///		An implemented Repository and UnitOfWork pattern should act like an in-memory collection
	///		(e.g. <see cref="List{T}"/>, <see cref="Dictionary{T1, T2}"/>), and therefore should not always have
	///		the same semantics of a database.
	///		
	///		<para>
	///			For example, rather than having an explicit Update method, you should instead get the <typeparamref name="TEntity"/>
	///			first and then update its properties before calling the save database method.
	///		</para>
	///		<example><code>
	///			var entity = await collection.GetAsync(1);
	///			entity.Name = "New name";
	///			entity.Age = 23;
	///		</code></example>
	/// </remarks>
	/// <typeparam name="TKey">The data type of the id property (i.e. the primary key in SQL databases) of <typeparamref name="TEntity"/>.</typeparam>
	/// <typeparam name="TEntity">The Model <see langword="class"/> this Repository works with.</typeparam>
	public interface IRepository<TKey, TEntity> where TEntity : class
	{
		/// <summary>
		///		Asynchronously retrieves a single <typeparamref name="TEntity"/> from the database.
		/// </summary>
		/// <param name="id">Key which identifies which <typeparamref name="TEntity"/> to retrieve.</param>
		/// <returns>
		///		A task result containing a <typeparamref name="TEntity"/>; otherwise result contains <see langword="null"/>
		///		if no <typeparamref name="TEntity"/> with this <paramref name="id"/> could be found.
		/// </returns>
		Task<TEntity?> GetAsync(TKey id);

		/// <summary>
		///		Asynchronously retrieves every <typeparamref name="TEntity"/> from the database.
		/// </summary>
		/// <returns>A task result containing a <see cref="IList{T}"/> of <typeparamref name="TEntity"/>.</returns>
		Task<IList<TEntity>> GetAllAsync();

		/// <summary>
		///		Asynchronously retrieves every <typeparamref name="TEntity"/> that matches the <paramref name="predicate"/> conditions.
		/// </summary>
		/// <param name="predicate">The filter condition written using the lambda syntax.</param>
		/// <returns>A task result containing a <see cref="IList{T}"/> of <typeparamref name="TEntity"/>.</returns>
		Task<IList<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>>? predicate);

		/// <summary>
		///		Asynchronously counts the number of rows in the database table. Use <paramref name="predicate"/>
		///		to exclude rows from the count.
		/// </summary>
		/// <param name="predicate">The filter condition written using the lambda syntax.</param>
		/// <returns>A task result containing the number of rows as <see cref="int"/>.</returns>
		Task<int> CountByFilterAsync(Expression<Func<TEntity, bool>>? predicate);

		/// <summary><inheritdoc cref="CountByFilterAsync(Expression{Func{TEntity, bool}}?)"/></summary>
		/// <typeparam name="T">The integer type to return.</typeparam>
		/// <param name="predicate"><inheritdoc cref="CountByFilterAsync(Expression{Func{TEntity, bool}}?)"/></param>
		/// <returns>A task result containing the number of rows as <typeparamref name="T"/>.</returns>
		Task<T> CountByFilterAsync<T>(Expression<Func<TEntity, bool>>? predicate) where T : IBinaryInteger<T>;

		/// <summary>
		///		Begins tracking the <typeparamref name="TEntity"/> that will be inserted into database table.
		/// </summary>
		/// <param name="entity">The row to be inserted.</param>
		void Add(TEntity entity);

		/// <summary>
		///		Begins tracking multiple <typeparamref name="TEntity"/> that will be inserted into the database table. 
		/// </summary>
		/// <param name="entities">The rows to be inserted.</param>
		void AddRange(IEnumerable<TEntity> entities);

		/// <summary>
		///		Begins tracking the <typeparamref name="TEntity"/> that will be deleted from database table.
		/// </summary>
		/// <param name="entity">The row to be deleted.</param>
		void Remove(TEntity entity);

		/// <summary>
		///		Begins tracking multiple <typeparamref name="TEntity"/> that will be deleted from the database table. 
		/// </summary>
		/// <param name="entities">The rows to be deleted.</param>
		void RemoveRange(IEnumerable<TEntity> entities);
	}
}
