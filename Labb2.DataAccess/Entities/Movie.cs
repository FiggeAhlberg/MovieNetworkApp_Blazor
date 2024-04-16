using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Labb2.DataAccess.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Genre> Genres { get; set; } = new();
    public int Duration { get; set; }
    public Director Director { get; set; }
    public DateTime ReleaseDate { get; set;}
    public int Price { get; set; }
    public ProductCategory ProductCategory { get; set; }

    public string? PosterImageUrl { get; set; }
    public string? BackgroundImageUrl { get; set; }
    public bool StatusAvailability { get; set; }
    [JsonIgnore]
    public virtual ICollection<MovieList> MovieLists { get; set; } = new List<MovieList>();

}