namespace Labb2.DataAccess.Entities;

public class Genre
{
	public int Id { get; set; }
	public string Name { get; set; }
	public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}