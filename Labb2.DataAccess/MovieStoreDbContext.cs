using Labb2.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Labb2.DataAccess;

public class MovieStoreDbContext : DbContext
{
	public DbSet<Movie> Movies { get; set; }

	public DbSet<User> Users { get; set; }

	public DbSet<MovieList> MovieLists { get; set; }

	public DbSet<Director> Directors { get; set; }

	public DbSet<Genre> Genres { get; set; }

	public DbSet<ProductCategory> ProductCategories { get; set; }
	public MovieStoreDbContext(DbContextOptions options) : base(options)
	{
		
	}

}