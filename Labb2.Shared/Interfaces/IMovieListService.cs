namespace Labb2.Shared.Interfaces;

public interface IMovieListService<T> where T : class
{
	Task<IEnumerable<T>> GetAllMovieLists();
	Task<T?> GetMovieListById(int id);
	Task<T?> GetMovieListByName(string name);
	Task AddMovieList(T newMovieList);

	Task AddMovieToMovieList(int listId, int movieId);

	Task UpdateMovieList(T movieList, int id);
	Task RemoveMovieList(int id);

	Task RemoveMovieFromMovieList(int listId, int movieId);
}