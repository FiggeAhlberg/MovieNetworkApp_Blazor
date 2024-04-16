namespace Labb2.Shared.DTOs;

public class GenreDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public override bool Equals(object o)
	{
		var other = o as GenreDto;
		return other?.Id == Id;
	}
	public override int GetHashCode() => Id.GetHashCode();
	public override string ToString()
	{
		return Name;
	}
}