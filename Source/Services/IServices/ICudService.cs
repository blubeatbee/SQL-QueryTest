namespace Source.Services.IServices
{
	public interface ICudService<T1, T2>
	{
		void CreateOneEntry(T2 newEntryDto);
		//void UpdateOneEntry(T1 id, T2 updateEntryDto);
		//void DeleteOneEntry(T1 id);
	}
}
