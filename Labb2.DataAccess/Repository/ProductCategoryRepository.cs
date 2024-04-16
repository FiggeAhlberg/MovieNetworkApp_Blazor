using Labb2.DataAccess.Entities;
using Labb2.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labb2.DataAccess.Repository;

public class ProductCategoryRepository : IProductCategoryService<ProductCategory>
{
	private readonly MovieStoreDbContext _context;

	public ProductCategoryRepository(MovieStoreDbContext context)
	{
		_context = context;
	}

	public async Task<ProductCategory> GetProductCategoryById(int id)
	{
		return await _context.ProductCategories.FindAsync(id);
	}
}