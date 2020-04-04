using Microsoft.Extensions.Caching.Memory;
using Mwh.Sample.Common.Models;
using Mwh.Sample.Common.Repositories;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Domain.Services;
using Supermarket.API.Domain.Services.Communication;
using Supermarket.API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Supermarket.API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMemoryCache _cache;


        public EmployeeService(IEmployeeRepository employeeRepository, IMemoryCache cache)
        {
            _employeeRepository = employeeRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<EmployeeModel>> ListAsync()
        {
            // Here I try to get the employees list from the memory cache. If there is no data in cache, the anonymous method will be
            // called, setting the cache to expire one minute ahead and returning the Task that lists the employees from the repository.
            var employees = await _cache.GetOrCreateAsync(CacheKeys.EmployeesList, (entry) =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _employeeRepository.ListAsync();
            }).ConfigureAwait(true);

            return employees;
        }

        public async Task<EmployeeResponse> SaveAsync(EmployeeModel employee)
        {
            try
            {
                await _employeeRepository.AddAsync(employee);

                return new EmployeeResponse(employee);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EmployeeResponse($"An error occurred when saving the employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> UpdateAsync(int id, EmployeeModel employee)
        {
            var existingEmployee = await _employeeRepository.FindByIdAsync(id);

            if (existingEmployee == null)
                return new EmployeeResponse("Employee not found.");

            existingEmployee.Name = employee.Name;

            try
            {
                _employeeRepository.Update(existingEmployee);

                return new EmployeeResponse(existingEmployee);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EmployeeResponse($"An error occurred when updating the employee: {ex.Message}");
            }
        }

        public async Task<EmployeeResponse> DeleteAsync(int id)
        {
            var existingEmployee = await _employeeRepository.FindByIdAsync(id);

            if (existingEmployee == null)
                return new EmployeeResponse("Employee not found.");

            try
            {
                _employeeRepository.Remove(existingEmployee);

                return new EmployeeResponse(existingEmployee);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new EmployeeResponse($"An error occurred when deleting the employee: {ex.Message}");
            }
        }
    }
}