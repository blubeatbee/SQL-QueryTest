namespace Source.Services.IServices
{
	public interface IService<TKey, TEntity>
	{
		void Create(TEntity newEntity);
		//void Update(TKey id);
		//void Delete(TKey id);
	}
}
