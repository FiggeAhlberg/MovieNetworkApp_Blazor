using Labb2.Shared.DTOs;
using Labb2.Shared.Interfaces;

namespace Labb2.Client.Services;

public class MovieListService : IMovieListService<AddMovieListDto>
{
	private readonly HttpClient _httpClient;

	public MovieListService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("movieNetworkApi");
	}

	public async Task<IEnumerable<AddMovieListDto>> GetAllMovieLists()
	{
		var response = await _httpClient.GetAsync("/movieLists");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<AddMovieListDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<List<AddMovieListDto>>();
		return result ?? Enumerable.Empty<AddMovieListDto>();
	}

	public async Task<AddMovieListDto?> GetMovieListById(int id)
	{
		var response = await _httpClient.GetAsync($"/movieLists/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<AddMovieListDto>();
		return result;
	}

	public async Task<AddMovieListDto?> GetMovieListByName(string name)
	{
		var response = await _httpClient.GetAsync($"/movieLists/{name}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<AddMovieListDto>();
		return result;
	}

	public async Task AddMovieList(AddMovieListDto newMovieList)
	{
		var response = await _httpClient.PostAsJsonAsync("/movieLists", newMovieList);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public Task AddMovieToMovieList(int listId, int movieId)
	{
		throw new NotImplementedException();
	}

	public async Task UpdateMovieList(AddMovieListDto updateMovieList, int id)
	{
		var response = await _httpClient.PutAsJsonAsync($"/movieLists/update/{id}", updateMovieList);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}


	public async Task RemoveMovieList(int id)
	{
		var response = await _httpClient.DeleteAsync($"/movieLists/{id}");

		if (!response.IsSuccessStatusCode)
		{
			throw new Exception("Failed to remove movielist");
		}
	}

	public Task RemoveMovieFromMovieList(int listId, int movieId)
	{
		throw new NotImplementedException();
	}
}