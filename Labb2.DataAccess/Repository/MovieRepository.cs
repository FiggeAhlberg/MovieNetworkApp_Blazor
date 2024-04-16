using Labb2.DataAccess.Entities;
using Labb2.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labb2.DataAccess.Repository;

public class MovieRepository : IMovieService<Movie>
{

	private readonly MovieStoreDbContext _context;

	public MovieRepository(MovieStoreDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Movie>> GetAllMovies()
    {
        return _context.Movies;
    }

   

    public async Task<Movie> GetMovieById(int id)
    {
        return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Movie> GetMovieByTitle(string title)
    {
        return await _context.Movies.FindAsync(title);
    }


    public async Task<List<Movie>> GetMovieByGenre(int genreId)
    {
        var movies = await _context.Movies
	        .Where(m => m.Genres.Any(g => g.Id == genreId))
	        .Include(m => m.Genres)
	        .ToListAsync();

        return movies;
    }

    public async Task AddMovie(Movie newMovie)
    {
	    _context.Movies.Add(newMovie);
	    await _context.SaveChangesAsync();
    }

	public async Task UpdateMovie(Movie updatedMovie, int id)
    {
	    _context.Movies.Update(updatedMovie);
        await _context.SaveChangesAsync();
    }


	public async Task RemoveMovie(int id)
    {
	   
        var movie = await _context.Movies.FindAsync(id);
        _context.Movies.Remove(movie);
		await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
	    await _context.SaveChangesAsync();
    }
}