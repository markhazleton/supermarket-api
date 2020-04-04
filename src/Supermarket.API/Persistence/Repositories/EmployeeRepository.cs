using Mwh.Sample.Common.Models;
using Mwh.Sample.Common.Repositories;
using Supermarket.API.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supermarket.API.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeMock _emp;
        public EmployeeRepository()
        {
            _emp = new EmployeeMock();
        }

        public async Task<IEnumerable<EmployeeModel>> ListAsync()
        {
            return await Task.Run(() => _emp.EmployeeCollection().ToArray()).ConfigureAwait(true);
            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task AddAsync(EmployeeModel employee)
        {
            await Task.Run(() => _emp.Update(employee)).ConfigureAwait(true);
        }

        public async Task<EmployeeModel> FindByIdAsync(int id)
        {
            return await Task.Run(() => _emp.Employee(id)).ConfigureAwait(true);
        }

        public void Update(EmployeeModel employee)
        {
            _emp.Update(employee);
        }

        public void Remove(EmployeeModel employee)
        {
            _emp.Delete(employee.EmployeeID);
        }
    }
}