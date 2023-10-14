namespace DAL.Repositories.IRepository
{
    public interface IGenericCrudRepository
    {
        Task<IEnumerable<T>> LoadData<T, U>(string SP, U parameters);
        Task<List<List<dynamic>>> LoadMultipleData<U>(string SP, U parameters);
        Task SaveData<T>(string SP, T parameters);
		Task<S?> SaveSingleQueryable<S, T>(string SP, T parameters);
        Task<IEnumerable<S>> SaveQueryable<S, T>(string SP, T parameters);
        Task<T> LoadSingleOrDefaultData<T, U>(string SP, U parameters);
	}
}
