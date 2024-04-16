using Labb2.Shared.DTOs;
using Labb2.Shared.Interfaces;

namespace Labb2.Client.Services;

public class UserService(IHttpClientFactory factory) : IUserService<UserDto>
{
	private readonly HttpClient _httpClient = factory.CreateClient("movieNetworkApi");
	

	public async Task<IEnumerable<UserDto>> GetAllUsers()
	{
		var response = await _httpClient.GetAsync("/users");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<UserDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<List<UserDto>>();
		return result ?? Enumerable.Empty<UserDto>();
	}

	public async Task<UserDto?> GetUserById(int id)
	{
		var response = await _httpClient.GetAsync($"/users/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<UserDto>();
		return result;
	}

	public async Task<UserDto?> GetUserByEmail(string email)
	{
		var response = await _httpClient.GetAsync($"/users/{email}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<UserDto>();
		return result;
	}

	public async Task AddUser(UserDto newUser)
	{
		var response = await _httpClient.PostAsJsonAsync("/users", newUser);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task UpdateUser(UserDto updatedUser)
	{
		var response = await _httpClient.PutAsJsonAsync("/users", updatedUser);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task RemoveUser(int id)
	{
		var response = await _httpClient.DeleteAsync($"/users/{id}");

		if (!response.IsSuccessStatusCode)
		{
			throw new Exception("Failed to remove user");
		}

		

	}
}