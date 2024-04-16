namespace Labb2.Shared.Interfaces;

public interface IMovieService<T> where T : class
{
	Task<IEnumerable<T>> GetAllMovies();
	Task<T?> GetMovieById(int id);
	Task<T?> GetMovieByTitle(string name);
	Task AddMovie(T newMovie);
	Task UpdateMovie(T updateMovie, int id);
	Task RemoveMovie(int id);
}