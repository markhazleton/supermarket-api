using Mwh.Sample.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeModel>> ListAsync();
        Task AddAsync(EmployeeModel employee);
        Task<EmployeeModel> FindByIdAsync(int id);
        void Update(EmployeeModel employee);
        void Remove(EmployeeModel employee);
    }
}