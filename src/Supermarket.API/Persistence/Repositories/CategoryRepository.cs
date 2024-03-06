using Microsoft.EntityFrameworkCore;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Persistence.Contexts;
using Supermarket.Domain.Models;
using Supermarket.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supermarket.API.Persistence.Repositories
{
    public class CategoryRepository(AppDbContext context) : BaseRepository(context), ICategoryRepository
    {

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> ListAsync()
            => await _context.Categories.AsNoTracking().ToListAsync();

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }
        public async Task AddAsync(Category category)
            => await _context.Categories.AddAsync(category);

        public async Task<Category?> FindByIdAsync(int id)
            => await _context.Categories.FindAsync(id);

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}