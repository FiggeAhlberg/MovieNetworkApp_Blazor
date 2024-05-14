using Labb2.Shared.DTOs;
using Labb2.Shared.Interfaces;

namespace Labb2.Client.Services;

public class MovieService : IMovieService<MovieDto>
{

	private readonly HttpClient _httpClient;

	public MovieService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("movieNetworkApi");
	}

	public async Task<IEnumerable<MovieDto>> GetAllMovies()
	{
		var response = await _httpClient.GetAsync("/movies");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<MovieDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<List<MovieDto>>();
		return result ?? Enumerable.Empty<MovieDto>();
	}

	

	public async Task<MovieDto?> GetMovieById(int id)
	{
		var response = await _httpClient.GetAsync($"/movies/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<MovieDto>();
		return result;
	}

	public async Task<MovieDto?> GetMovieByTitle(string name)
	{
		var response = await _httpClient.GetAsync($"/movies/{name}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<MovieDto>();
		return result;
	}



	public async Task AddMovie(MovieDto newMovie)
	{
		var response = await _httpClient.PostAsJsonAsync("/movies", newMovie);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task UpdateMovie(MovieDto updateMovie, int id)
	{
		var response = await _httpClient.PutAsJsonAsync($"/movies/update/{id}", updateMovie);

		if (!response.IsSuccessStatusCode)
		{
			throw new Exception("Could not update movie");
		}
	}

	public async Task RemoveMovie(int id)
	{
		var response = await _httpClient.DeleteAsync($"/movies/{id}");

		if (!response.IsSuccessStatusCode)
		{
			throw new Exception("Failed to remove movie");
		}

	}
}