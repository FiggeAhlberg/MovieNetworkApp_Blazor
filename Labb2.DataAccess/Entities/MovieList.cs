namespace Labb2.DataAccess.Entities;

public class MovieList
{
	public int Id { get; set; }
	public string Name { get; set; }
	public DateTime CreationDate { get; set; }
	public List<Movie> Movies { get; set; } = new();
	public User User { get; set; }
}