using Labb2.DataAccess.Entities;
using Labb2.Shared.Interfaces;

namespace Labb2.DataAccess.Repository;

public class GenreRepository: IGenreService<Genre>
{

	private readonly MovieStoreDbContext _context;

	public GenreRepository(MovieStoreDbContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<Genre>> GetAllGenres()
	{
		return _context.Genres;
	}

	public async Task<Genre> GetGenreById(int id)
	{
		return await _context.Genres.FindAsync(id);
	}
}