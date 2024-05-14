using Labb2.Shared.DTOs;
using System.Net.Http;
using Labb2.Shared.Interfaces;

namespace Labb2.Client.Services;

public class TmdbService: ITmdbService<TmdbMovieDto>
{

	private readonly HttpClient _httpClient;

	public TmdbService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<IEnumerable<TmdbMovieDto>> GetMovieFromTmdb()
	{
		var apiKey = "936fc4084d3ed69d2ed54442b8abc7b7";
		var movieUrl = $"https://api.themoviedb.org/3/movie/top_rated?api_key={apiKey}";

		var response = await _httpClient.GetAsync($"{movieUrl}");
		var result = await response.Content.ReadFromJsonAsync<TmdbMovieResponse>();

		var movies = new List<TmdbMovieDto>();

		foreach (var movie in result.Results)
		{
			var tmdbMovie = new TmdbMovieDto()
			{
				Id = movie.Id,
				Title = movie.Title,
				Poster_Path = movie.Poster_Path,
				Backdrop_Path = movie.Backdrop_Path
			};

			movies.Add(tmdbMovie);
		}

		return movies;
	}
}