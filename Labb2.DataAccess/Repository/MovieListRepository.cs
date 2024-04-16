using Labb2.DataAccess.Entities;
using Labb2.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Labb2.DataAccess.Repository;

public class MovieListRepository : IMovieListService<MovieList>
{

	private readonly MovieStoreDbContext _context;

	public MovieListRepository(MovieStoreDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<MovieList>> GetAllMovieLists()
	{
		return _context.MovieLists.Include(ml => ml.Movies);
	}

	public async Task<MovieList> GetMovieListById(int id)
	{
		return await _context.MovieLists.Include(ml => ml.Movies).FirstOrDefaultAsync(ml => ml.Id == id);
	}

	public async Task<MovieList> GetMovieListByName(string name)
	{
		return await _context.MovieLists.FirstOrDefaultAsync(ml => ml.Name == name);
	}

	public async Task AddMovieList(MovieList newMovieList)
	{
		_context.MovieLists.Add(newMovieList);
		await _context.SaveChangesAsync();
	}

	public async Task AddMovieToMovieList(int listId, int movieId)
	{
		var movieList = await _context.MovieLists.FindAsync(listId);
		var movie = await _context.Movies.FindAsync(movieId);
		movieList.Movies.Add(movie);
		await _context.SaveChangesAsync();
	}


	public async Task UpdateMovieList(MovieList updatedMovieList, int id)
	{
		_context.MovieLists.Update(updatedMovieList);
		await _context.SaveChangesAsync();

	}



	public async Task RemoveMovieList(int id)
	{
		var removeMovieList = await _context.MovieLists.FindAsync(id);
		_context.MovieLists.Remove(removeMovieList);
		await _context.SaveChangesAsync();
	}

	public async Task RemoveMovieFromMovieList(int listId, int movieId)
	{
		var movieList = await _context.MovieLists.Include(ml=>ml.Movies).FirstOrDefaultAsync(ml => ml.Id ==listId);
		var movie = await _context.Movies.FindAsync(movieId);
		movieList.Movies.Remove(movie);
		await _context.SaveChangesAsync();
	}
}