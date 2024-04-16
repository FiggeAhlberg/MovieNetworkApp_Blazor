using Labb2.DataAccess.Entities;
using Labb2.Shared.Interfaces;

namespace Labb2.API.Extensions;

public static class ProductCategoryEndpointExtension
{
	public static IEndpointRouteBuilder MapProductCategoryEndpoint(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("productCategories");

		group.MapGet("/{id}", GetProductCategoryById);

		return app;
	}

	private static async Task<IResult> GetProductCategoryById(IProductCategoryService<ProductCategory> repo, int id)
	{
		var productCategory = await repo.GetProductCategoryById(id);

		if (productCategory is null)
		{
			return Results.NotFound($"ProductCategory with id: {id} doesn't exist");
		}

		return Results.Ok(productCategory);
	}
}