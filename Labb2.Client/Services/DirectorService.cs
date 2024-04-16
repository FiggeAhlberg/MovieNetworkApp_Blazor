using Labb2.Shared.DTOs;
using Labb2.Shared.Interfaces;

namespace Labb2.Client.Services;

public class DirectorService: IDirectorService<DirectorDto>
{
	private readonly HttpClient _httpClient;

	public DirectorService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("movieNetworkApi");
	}

	public async Task<IEnumerable<DirectorDto>> GetAllDirectors()
	{
		var response = await _httpClient.GetAsync("/directors");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<DirectorDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<List<DirectorDto>>();
		return result ?? Enumerable.Empty<DirectorDto>();
	}

	public async Task<DirectorDto?> GetDirectorById(int id)
	{
		var response = await _httpClient.GetAsync($"/directors/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<DirectorDto>();
		return result;
	}

	public async Task<DirectorDto?> GetDirectorByFirstName(string firstName)
	{
		var response = await _httpClient.GetAsync($"/directors/{firstName}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<DirectorDto>();
		return result;
	}

	public async Task<DirectorDto?> GetDirectorByLastName(string lastName)
	{
		var response = await _httpClient.GetAsync($"/directors/{lastName}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<DirectorDto>();
		return result;
	}

	public async Task AddDirector(DirectorDto newDirector)
	{
		var response = await _httpClient.PostAsJsonAsync("/directors", newDirector);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task UpdateDirector(DirectorDto updateDirector, int id)
	{
		var response = await _httpClient.PutAsJsonAsync("/directors", updateDirector);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task RemoveDirector(int id)
	{
		var response = await _httpClient.DeleteAsync($"/directors/{id}");

		if (!response.IsSuccessStatusCode)
		{
			throw new Exception("Failed to remove director");
		}
	}
}