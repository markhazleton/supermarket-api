using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API.Persistence.Repositories
{
    public class UnitOfWork(AppDbContext context) : Supermarket.Domain.Repositories.IUnitOfWork
    {
        private readonly AppDbContext _context = context;

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}