using Supermarket.Domain.Models;
using Supermarket.Domain.Models.Queries;
using Supermarket.Domain.Services.Communication;
using System.Threading.Tasks;

namespace Supermarket.Domain.Services
{
    public interface IProductService
    {
        Task<QueryResult<Product>> ListAsync(ProductsQuery query);
        Task<ProductResponse> SaveAsync(Product product);
        Task<ProductResponse> UpdateAsync(int id, Product product);
        Task<ProductResponse> DeleteAsync(int id);
    }
}