using Labb2.Shared.DTOs;
using Labb2.Shared.Interfaces;

namespace Labb2.Client.Services;

public class ProductCategoryService(IHttpClientFactory factory) : IProductCategoryService<ProductCategoryDto>
{
	private readonly HttpClient _httpClient = factory.CreateClient("movieNetworkApi");

	public async Task<ProductCategoryDto?> GetProductCategoryById(int id)
	{
		var response = await _httpClient.GetAsync("/productCategories{id}");

		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<ProductCategoryDto>();
		return result;
	}
}