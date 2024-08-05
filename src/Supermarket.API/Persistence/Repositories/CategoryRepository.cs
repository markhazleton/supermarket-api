using Microsoft.EntityFrameworkCore;
using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API.Persistence.Repositories
{
    public class CategoryRepository(AppDbContext context) : BaseRepository(context), Supermarket.Domain.Repositories.ICategoryRepository
    {

        public async Task AddAsync(Supermarket.Domain.Models.Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<Supermarket.Domain.Models.Category> FindByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Supermarket.Domain.Models.Category>> ListAsync()
            => await _context.Categories.AsNoTracking().ToListAsync();

        public void Remove(Supermarket.Domain.Models.Category category)
        {
            _context.Categories.Remove(category);
        }
        public void Update(Supermarket.Domain.Models.Category category)
        {
            _context.Categories.Update(category);
        }
    }
}