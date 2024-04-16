namespace Labb2.Shared.Interfaces;

public interface IProductCategoryService<T> where T : class
{
	Task<T?> GetProductCategoryById(int id);
}