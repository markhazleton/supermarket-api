namespace Supermarket.Domain.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Models.Category>> ListAsync();
    Task AddAsync(Models.Category category);
    Task<Models.Category?> FindByIdAsync(int id);
    void Update(Models.Category category);
    void Remove(Models.Category category);
}