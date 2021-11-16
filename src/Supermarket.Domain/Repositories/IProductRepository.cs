using Supermarket.Domain.Models;
using Supermarket.Domain.Models.Queries;
using System.Threading.Tasks;

namespace Supermarket.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<QueryResult<Product>> ListAsync(ProductsQuery query);
        Task AddAsync(Product product);
        Task<Product> FindByIdAsync(int id);
        void Update(Product product);
        void Remove(Product product);
    }
}