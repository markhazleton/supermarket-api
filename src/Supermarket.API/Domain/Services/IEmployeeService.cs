using Mwh.Sample.Common.Models;
using Supermarket.API.Domain.Services.Communication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supermarket.API.Domain.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeModel>> ListAsync();
        Task<EmployeeResponse> SaveAsync(EmployeeModel employee);
        Task<EmployeeResponse> UpdateAsync(int id, EmployeeModel employee);
        Task<EmployeeResponse> DeleteAsync(int id);
    }
}