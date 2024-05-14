namespace Labb2.Shared.Interfaces;

public interface ITmdbService<T> where T : class
{
	Task<IEnumerable<T>> GetMovieFromTmdb();
}