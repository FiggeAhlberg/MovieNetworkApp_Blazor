namespace Labb2.Shared.Interfaces;

public interface IDirectorService<T> where T : class
{
	Task<IEnumerable<T>> GetAllDirectors();
	Task<T?> GetDirectorById(int id);
	Task<T?> GetDirectorByFirstName(string firstName);
	Task<T?> GetDirectorByLastName(string lastName);
	Task AddDirector(T newDirector);
	Task UpdateDirector(T updateDirector, int id);
	Task RemoveDirector(int id);
}