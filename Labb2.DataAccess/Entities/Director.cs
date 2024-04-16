using System.Security.AccessControl;

namespace Labb2.DataAccess.Entities;

public class Director
{
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public DateTime BirthDate { get; set; }
	public List<Movie> Movies { get; set; } = new();
}

