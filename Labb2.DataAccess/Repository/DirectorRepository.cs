using Labb2.DataAccess.Entities;
using Labb2.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labb2.DataAccess.Repository;

public class DirectorRepository : IDirectorService<Director>
{

	private readonly MovieStoreDbContext _context;

	public DirectorRepository(MovieStoreDbContext context)
	{
		_context = context;
	}

	public async Task AddDirector(Director newDirector)
	{
		_context.Directors.Add(newDirector);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<Director>> GetAllDirectors()
	{
		return _context.Directors;
	}

	public async Task<Director> GetDirectorById(int id)
	{
		return await _context.Directors.Include(d => d.Movies).FirstOrDefaultAsync(d => d.Id == id);
	}

	public async Task<Director> GetDirectorByFirstName(string firstName)
	{
		return await _context.Directors.FirstOrDefaultAsync(d => d.FirstName == firstName);
	}

	public async Task<Director> GetDirectorByLastName(string lastName)
	{
		return await _context.Directors.FirstOrDefaultAsync(d => d.LastName == lastName);
	}

	public async Task UpdateDirector(Director updateDirector, int id)
	{
		_context.Directors.Update(updateDirector);
		await _context.SaveChangesAsync();
	}

	public async Task RemoveDirector(int id)
	{
		var removeDirector = await _context.Directors.FindAsync(id);
		_context.Directors.Remove(removeDirector);
		await _context.SaveChangesAsync();
	}
}