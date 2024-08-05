using Supermarket.Domain.Models;
using Supermarket.Domain.Services.Communication;

namespace Supermarket.Domain.Services;

public interface ICategoryService
{
     Task<IEnumerable<Category>> ListAsync();
     Task<Response<Category>> SaveAsync(Category category);
     Task<Response<Category>> UpdateAsync(int id, Category category);
     Task<Response<Category>> DeleteAsync(int id);
}