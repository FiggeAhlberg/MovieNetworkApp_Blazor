namespace Labb2.Shared.DTOs;

public class MovieDto
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public List<int> GenreIds { get; set; } = new();
	public int ProductCategoryId { get; set; } 
	public int DirectorId { get; set; }
	public int Duration { get; set; }

	public string PosterImageUrl { get; set; } = string.Empty;
	public string BackgroundImageUrl { get; set; } = string.Empty;
}