using Labb2.Shared.DTOs;
using Labb2.Shared.Interfaces;

namespace Labb2.Client.Services;

public class GenreService : IGenreService<GenreDto>
{

	private readonly HttpClient _httpClient;

	public GenreService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("movieNetworkApi");
	}
	public async Task<IEnumerable<GenreDto>> GetAllGenres()
	{
		var response = await _httpClient.GetAsync("/genres");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<GenreDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<List<GenreDto>>();
		return result ?? Enumerable.Empty<GenreDto>();
	}

	public async Task<GenreDto?> GetGenreById(int id)
	{
		var response = await _httpClient.GetAsync($"/genres/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<GenreDto>();
		return result;
	}
}