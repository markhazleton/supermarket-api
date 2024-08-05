using Microsoft.Extensions.Caching.Memory;
using Supermarket.API.Infrastructure;
using Supermarket.Domain.Models;
using Supermarket.Domain.Services.Communication;

namespace Supermarket.API.Services;

public class CategoryService(
        Supermarket.Domain.Repositories.ICategoryRepository categoryRepository,
        Supermarket.Domain.Repositories.IUnitOfWork unitOfWork,
        IMemoryCache cache,
        ILogger<CategoryService> logger
        ) : Domain.Services.ICategoryService
{
        public async Task<IEnumerable<Category>> ListAsync()
	{
		// Here I try to get the categories list from the memory cache. If there is no data in cache, the anonymous method will be
		// called, setting the cache to expire one minute ahead and returning the Task that lists the categories from the repository.
		var categories = await cache.GetOrCreateAsync(CacheKeys.CategoriesList, (entry) =>
		{
			entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
			return categoryRepository.ListAsync();
		});
		return categories ?? [];
	}

	public async Task<Response<Category>> SaveAsync(Category category)
	{
		try
		{
			await categoryRepository.AddAsync(category);
			await unitOfWork.CompleteAsync();

			return new Response<Category>(category);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Could not save category.");
			return new Response<Category>($"An error occurred when saving the category: {ex.Message}");
		}
	}

	public async Task<Response<Category>> UpdateAsync(int id, Category category)
	{
		var existingCategory = await categoryRepository.FindByIdAsync(id);
		if (existingCategory == null)
		{
			return new Response<Category>("Category not found.");
		}
		existingCategory.Name = category.Name;

		try
		{
			await unitOfWork.CompleteAsync();
			return new Response<Category>(existingCategory);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Could not update category with ID {id}.", id);
			return new Response<Category>($"An error occurred when updating the category: {ex.Message}");
		}
	}

	public async Task<Response<Category>> DeleteAsync(int id)
	{
		var existingCategory = await categoryRepository.FindByIdAsync(id);
		if (existingCategory == null)
		{
			return new Response<Category>("Category not found.");
		}

		try
		{
			categoryRepository.Remove(existingCategory);
			await unitOfWork.CompleteAsync();

			return new Response<Category>(existingCategory);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Could not delete category with ID {id}.", id);
			return new Response<Category>($"An error occurred when deleting the category: {ex.Message}");
		}
	}
}
