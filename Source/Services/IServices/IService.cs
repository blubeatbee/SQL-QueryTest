namespace Source.Services.IServices
{
	public interface IService<TKey, TEntity> where TEntity : class
	{
		void Create(TEntity newEntity);
		//void Update(TKey id);
		//void Delete(TKey id);
	}
}
