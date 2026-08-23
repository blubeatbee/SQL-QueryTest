namespace Source.Repositories.IRepositories
{
	public interface IRepository<TEntity, TEntityKey>
	{
		Task<bool> Exists(TEntityKey id);
		Task<TEntity?> FindOne(TEntityKey id);
		IQueryable<TEntity>? FindAll();

		Task Delete(TEntity entry);
		Task Insert(TEntity entry);
		Task Update(TEntity entry);
	}
}
