namespace Labb2.Shared.Interfaces;

public interface IGenreService<T> where T : class
{
	Task<IEnumerable<T>> GetAllGenres();
	Task<T?> GetGenreById(int id);
}